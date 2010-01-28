
using NUnit.Framework;
using Driven.Metrics.Tests.TestBuilders;
using Driven.Metrics.Reporting;

namespace Driven.Metrics.Tests.Reporting
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
