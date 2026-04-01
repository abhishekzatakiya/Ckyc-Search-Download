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
    public class Test040_CkycSearch_NumberAndReferenceId : BaseTest
    {
        public static IEnumerable Data()
        {
            yield return new TestCaseData("90905678905896", "INDVVV32552663", true);
        }

        [Test]
        [Description("Verify CKYC Search using CKYC Number + Reference ID")]
        [Category("CKYC Search")]
        [Property("Feature", "CKYC")]
        [Property("Component", "Search And Download")]
        [TestCaseSource(nameof(Data))]
        public async Task AT13_VerifyCkycSearch_NumberAndReferenceId(
            string ckycNumber,
            string referenceId,
            bool expectResults)
        {
            var loginHelper = new LoginHelper(page);
            var helper = new QuickFetchHelper(page);

            try
            {
                // Login
                await loginHelper.LoginUserConsole();

                // Navigate
                await helper.NavigateToSearchAndDownload();
                await helper.ClickQuickFetch();

                // Enter both fields
                await helper.EnterCkycNumber(ckycNumber);
                await helper.EnterCkycReferenceId(referenceId);

                // Search
                await helper.ClickSearch();

                // Validate
                var isVisible = await helper.AreSearchResultsVisible();

                Assert.That(isVisible, Is.EqualTo(expectResults),
                    $"Search failed for CKYC: {ckycNumber} + RefID: {referenceId}");
            }
            catch
            {
                ReportError();
                throw;
            }
        }
    }
}