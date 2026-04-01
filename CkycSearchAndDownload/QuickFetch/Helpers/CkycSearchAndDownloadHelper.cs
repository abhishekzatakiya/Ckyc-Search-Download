using Microsoft.Playwright;
using TrackWizzSaaS.Core.Test.Support.Selectors;
using TrackWizzSaaS.Core.Test.Support.PageLoader;
using TrackWizzSaaS.Core.Test.Support.Halt;
using System.Threading.Tasks;

namespace TrackWizzSaaS.Ckyc.Test.CkycSearchAndDownload.QuickFetch.Helpers;

internal class QuickFetchHelper
{
    private readonly IPage _page;
    private readonly SelectorsStore _selector;
    private readonly PageLoaderHelper _pageLoader;

    public QuickFetchHelper(IPage page)
    {
        _page = page;
        _selector = new SelectorsStore();
        _pageLoader = new PageLoaderHelper(page);
    }

    #region Locators
    private ILocator _ckycMenu => _page.Locator("//a[@id='CKYC']/span[contains(text(),'CKYC')]");
    private ILocator _searchAndDownloadMenu => _page.Locator("//span[contains(text(),'Search & Download')]");
    private ILocator _quickFetch => _page.Locator("//span[contains(text(),'Quick Fetch')]");
    private ILocator _bulkFetch => _page.Locator("//span[contains(text(),'Bulk Fetch')]");
    
    //common locator for both
    private ILocator _ckycNumberInput => _page.Locator("#ckycNumber");
    private ILocator _ckycReferenceIdInput => _page.Locator("#ckycReferenceId");
    private ILocator _enterPanInput => _page.Locator("#pan");
    //locator for individual
    private ILocator _voterCardIdInput => _page.Locator("#voterId");
    private ILocator _passportIput => _page.Locator("#passport");
    private ILocator _drivingLicenceInput => _page.Locator("#drivingLicense");

    //Aadhar Search Locator 
    private ILocator _aadhaarLastFourDigitsInput => _page.Locator("#aadhaarLastFourDigits");
    private ILocator _fullNameInput => _page.Locator("#fullName");
    private ILocator _dateOfBirthInput => _page.Locator("#dateOfBirth");
    private ILocator _genderInput => _page.Locator("#gender");

    //locator for LE
    private ILocator _cinNumberInput => _page.Locator("#cinNumber");
    private ILocator _crnNumberInput => _page.Locator("#crnNumber");

    private ILocator _searchButton => _page.Locator("#search-button");
    private ILocator _searchResultsRows => _page.Locator("//div[contains(@class,'search-results-entered-data')]//div");

    #endregion

    #region Navigation

    public async Task NavigateToSearchAndDownload()
    {
        await _ckycMenu.ClickAsync();

        await _searchAndDownloadMenu.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await _searchAndDownloadMenu.ClickAsync();

        await _pageLoader.WaitForSpinnerToDisappear();
    }

    #endregion

    #region Tab Actions

    public async Task ClickQuickFetch()
    {
        await _quickFetch.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await _quickFetch.ClickAsync();
        await _pageLoader.WaitForSpinnerToDisappear();
    }

    public async Task ClickBulkFetch()
    {
        await _bulkFetch.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await _bulkFetch.ClickAsync();
        HaltUtil.HaltFor1Seconds();
    }

    #endregion

    #region Search Actions

    public async Task EnterCkycNumber(string ckycNumber)
    {
        await _ckycNumberInput.FillAsync(ckycNumber);
    }

    public async Task EnterCkycReferenceId(string ckycReferenceId)
    {
        await _ckycReferenceIdInput.FillAsync(ckycReferenceId);
    }
    public async Task EnterPanNumber(string panNumber)
    {
        await _enterPanInput.FillAsync(panNumber);
    }

    public async Task ClickSearch()
    {
        await _searchButton.ClickAsync();
        await _pageLoader.WaitForSpinnerToDisappear();
    }



    public async Task<bool> AreSearchResultsVisible()
    {
        try
        {
            // Wait for the container and at least one child div to appear
            await _searchResultsRows.First.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 30000
            });

            // Count all result divs
            var count = await _searchResultsRows.CountAsync();
            return count > 0;
        }
        catch
        {
            // Take screenshot for debugging if results are not visible
            await _page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = $"Screenshots/Debug_Failed_{DateTime.Now:yyyyMMdd_HHmmss}.png",
                FullPage = true
            });
            return false;
        }
    }

    #endregion
}