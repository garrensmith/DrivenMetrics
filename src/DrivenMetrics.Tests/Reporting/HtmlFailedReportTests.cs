
using NUnit.Framework;
using DrivenMetrics.Tests.TestBuilders;
using DrivenMetrics.Reporting;

namespace DrivenMetrics.Tests.Reporting
{

	[TestFixture]
	public class HtmlFailedReportTests
	{

		[Test]
        public void ShouldWriteMetricInfoIntoBody()
        {
            var results = new MetricResultBuilder().CreateMetricResult();
            var fileWriter = new FileWriter();
            var report = new HtmlFailedReport(fileWriter,"reportFailedtest.html");
			
            Assert.DoesNotThrow(() => report.Generate(results));
        }
	}
}
