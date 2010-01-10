
using DrivenMetrics.Metrics;

namespace DrivenMetrics.Reporting
{

	public class HtmlFailedReport : HtmlReport, IReport
	{
		protected override string addResult(MethodResult methodResult)
        {
            if (!methodResult.Pass)
                return @"<td id =""fail"">" + methodResult.Result + "</td>";
			
			return string.Empty;
        }

		public HtmlFailedReport (IFileWriter fileWriter, string filePath) : base(fileWriter, filePath)
		{
			_ReportName = "Driven Metric Failing Methods";
		}
	}
}
