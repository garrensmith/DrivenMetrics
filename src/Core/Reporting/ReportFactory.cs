
using System;

namespace Driven.Metrics.Reporting
{
	public class ReportFactory
	{
		public IReport ResolveReport (Driven.Metrics.Reporting.ReportType reportType, string reportName)
		{
			var fileWriter = new FileWriter();
			IReport report = null;
			
			if (reportType == ReportType.Failing)
				report = new HtmlFailedReport(fileWriter, reportName);
			else
				report = new HtmlReport(fileWriter,reportName);
			
			return report;
		}
		
	}
}
