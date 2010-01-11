
using System;
using NUnit.Framework;
using DrivenMetrics.UI.Console;
using DrivenMetrics.Reporting;

namespace DrivenMetrics.Tests.Console
{
	

	[TestFixture]
	public class ConsoleBootstrappingTests
	{

		[Test]
		public void ShouldBootStrapCorrectFailingReport()
		{
			var bootStrapper = new ReportFactory();
			
			var report = bootStrapper.ResolveReport(ReportType.Failing, "fake.html");
			
			Assert.That(report.GetType(), Is.EqualTo(typeof(HtmlFailedReport)));			
		}
		
		[Test]
		public void ShouldBootStrapCorrectAllReport()
		{
			var bootStrapper = new ReportFactory();
			
			var report = bootStrapper.ResolveReport(ReportType.All, "fake.html");
			
			Assert.That(report.GetType(), Is.EqualTo(typeof(HtmlReport)));	
			
		}
	}
}
