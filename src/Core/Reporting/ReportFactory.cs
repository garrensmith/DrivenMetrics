
namespace Driven.Metrics.Reporting
{
	public class ReportFactory
	{
		public IReport ResolveReport (ReportType reportType, string reportName)
		{
			var fileWriter = new FileWriter();
			IReport report = null;
			
			if (reportType == ReportType.Failing)
				report = new HtmlFailedReport(fileWriter, reportName);
            else if (reportType == ReportType.TopTen)
                report = new HtmlTopTenReport(fileWriter, reportName);
            else if (reportName.EndsWith (".xml"))
                report = new XmlReport (fileWriter, reportName);
            else
				report = new HtmlReport(fileWriter,reportName);
			
			return report;
		}
		
	}
}
