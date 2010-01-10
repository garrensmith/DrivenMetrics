using DrivenMetrics.Reporting;
using DrivenMetrics.Tests.TestBuilders;
using NUnit.Framework;

namespace DrivenMetrics.Tests.Reporting
{
    [TestFixture()]
    public class Test
    {

        /*[Test()]
		public void ShouldGenerateHeadAndTail()
		{
			var fileWriter = MockRepository.GenerateMock<IFileWriter>();
			
			var report = new HtmlReport(fileWriter);
			
			report.Generate();
			
			string html = @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01//EN""""http://www.w3.org/TR/html4/strict.dtd""><HTML><HEAD><TITLE>Driven Metric Report</TITLE></HEAD><BODY></BODY></HTML>";
			
			Assert.AreEqual(html, report.Contents);
			
		}
		
		[Test]
		public void ShouldWriteContentsToFileWriter()
		{
			var fileWriter = MockRepository.GenerateMock<IFileWriter>();
			
			var report = new HtmlReport(fileWriter);
			
			report.Generate();
			
			fileWriter.AssertWasCalled(x => x.Write("",""), options => options.IgnoreArguments());
		}*/
		
        [Test]
        public void ShouldWriteMetricInfoIntoBody()
        {
            var results = new MetricResultBuilder().CreateMetricResult();
            var fileWriter = new FileWriter();
            var report = new HtmlReport(fileWriter,"test2.html");
			
            Assert.DoesNotThrow(() => report.Generate(results));
        }
    }
}