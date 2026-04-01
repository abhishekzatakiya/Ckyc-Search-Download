using Microsoft.Playwright;
using NUnit.Framework;
using System.Collections;
using System.Threading.Tasks;
using TrackWizzSaaS.Ckyc.Test.CkycSearchAndDownload.QuickFetch.Helpers;
using TrackWizzSaaS.Core.Test;
using TrackWizzSaaS.Core.Test.BaseFeatures.Login;

namespace TrackWizzSaaS.Ckyc.Test.CkycSearchAndDownload.TestCases
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Test030_CkycSearchByReferenceID : BaseTest
    {
        public static IEnumerable CkycReferenceIDSearchData()
        {
            yield return new TestCaseData("INDVVV32552663", true);
        }

        [Test]
        [Description("Verify CKYC Search by CKYC Number works correctly")]
        [Category("CKYC Search")]
        [Property("Feature", "CKYC")]
        [Property("Component", "Search And Download")]
        [TestCaseSource(nameof(CkycReferenceIDSearchData))]
        public async Task AT03_VerifyCkycReferenceIDSearch(string ckycReferenceId, bool expectResults)
        {
            var loginHelper = new LoginHelper(page);
            var ckycHelper = new QuickFetchHelper(page);

            try
            {
                // Login (continues on your custom URL)
                await loginHelper.LoginUserConsole();

                // Navigate to CKYC -> Search & Download
                await ckycHelper.NavigateToSearchAndDownload();

                // Select Quick Fetch tab
                await ckycHelper.ClickQuickFetch();

                // Enter CKYC Number and Search
                await ckycHelper.EnterCkycReferenceId(ckycReferenceId);
                await ckycHelper.ClickSearch();

                // Better wait strategy (replace selector accordingly)
                await page.WaitForLoadStateAsync(LoadState.NetworkIdle);


                // Validate results
                var isResultsVisible = await ckycHelper.AreSearchResultsVisible();

                Assert.That(isResultsVisible, Is.EqualTo(expectResults),
                    $"Search results visibility expected to be {expectResults} for CKYC number: {ckycReferenceId}");
            }
            catch
            {
                ReportError();
                throw;
            }
        }
    }
}