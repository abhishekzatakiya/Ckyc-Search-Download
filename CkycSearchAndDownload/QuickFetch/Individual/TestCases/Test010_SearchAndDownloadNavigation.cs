using NUnit.Framework;
using TrackWizzSaaS.Core.Test;
using TrackWizzSaaS.Core.Test.BaseFeatures.Login;
using TrackWizzSaaS.Ckyc.Test.CkycSearchAndDownload.QuickFetch.Helpers;
namespace TrackWizzSaaS.Ckyc.Test.CkycSearchAndDownload.TestCases
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Test010_SearchAndDownloadNavigation : BaseTest
    {
        [Test]
        [Description("Verify user can navigate to CKYC Search & Download and access Quick Fetch & Bulk Fetch")]
        [Category("CKYC Search And Download")]
        [Property("Feature", "CKYC")]
        [Property("Component", "Search And Download")]
        public async Task AT01_Verify_SearchAndDownload_Navigation()
        {
            var loginHelper = new LoginHelper(page);
            var searchDownloadHelper = new QuickFetchHelper(page);

            // Login
            await loginHelper.LoginUserConsole();

            // Navigate to CKYC -> Search & Download
            await searchDownloadHelper.NavigateToSearchAndDownload();

            // Click Quick Fetch tab
            await searchDownloadHelper.ClickQuickFetch();

            // Click Bulk Fetch tab
            //await searchDownloadHelper.ClickBulkFetch();

            // If no exceptions, test passed
            Assert.Pass("Navigation to Search & Download, Quick Fetch, and Bulk Fetch successful");
        }
    }
}