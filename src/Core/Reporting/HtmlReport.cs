using System.Collections.Generic;
using DrivenMetrics.Metrics;

namespace DrivenMetrics.Reporting
{
    public class HtmlReport : IReport
    {
        private readonly IFileWriter _fileWriter;
		private readonly string _filePath;
        private const string _htmlHeader = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD HTML 4.01//EN""
										   ""http://www.w3.org/TR/html4/strict.dtd"">
										
										<html lang=""en"">
										<head>
											<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"">
											<title>Driven Metric Report</title>
											<style type=""text/css"">
												body {
													font-family: Verdana, Arial, Helvetica, sans-serif;
												}
												th {
													background-color: gray;
												}
												h2 {
													 border-bottom-style: solid;
												}
												
												#fail {
													background-color:red;
													
												}
												#pass 
												{
													background-color:green;
												}
												td
												{
													min-width:230px;
												}
												
											</style>
										</head>
										<body>";
											      
        private const string _htmlTail = @"	</BODY></HTML>";
        private const string _emptyColumn = @"<td>&nbsp</td>";
		protected string _ReportName = "Driven Metric Report";
		
        public string Contents {get; private set;}
        public string Body {get; private set;}
		
		
        public HtmlReport(IFileWriter fileWriter, string filePath)
        {
            _fileWriter = fileWriter;
			_filePath = filePath;
			
        }
		
        public void Generate(IEnumerable<MetricResult> results)
        {
            addHtmlHeader();
			
            foreach(var result in results)
                convertResultToHtml(result);
			
            addHtmlTail();
			
            _fileWriter.Write(_filePath,Contents);
        }
		
        public void Generate(MetricResult result)
        {
            Generate(new[] {result});
        }
		
        protected virtual void convertResultToHtml(MetricResult result)
        {
			
            Contents += "<h2>" + result.Name + "</h2>";
            Contents += @"<table border=""1"">
							<tr>
							<th>Class</th>
							<th>Method</th>
							<th>Result</th>
							</tr>";
			
            foreach(var classResult in result.ClassResults)
            {
                Contents += "<tr>";
                Contents += "<td>" + classResult.Name +"</td>";
                Contents += _emptyColumn + _emptyColumn + "</tr>";
				
                foreach(var methodResult in classResult.MethodResults)
                {
                    Contents += _emptyColumn;
                    Contents += "<td>"+ methodResult.Name +"</td>";
                    Contents += addResult(methodResult);
                    Contents += "</tr>";
                }
            }

            Contents += endOfTable();
        }

        private string endOfTable()
        {
            return "</table>";
        }

        protected virtual string addResult(MethodResult methodResult)
        {
            if (methodResult.Pass)
                return @"<td id =""pass"">"+ methodResult.Result +"</td>";
	        
            return @"<td id =""fail"">" + methodResult.Result + "</td>";
        }

        private void addHtmlHeader()
        {
            Contents = _htmlHeader;
			Contents += "<h1>" + _ReportName + "</h1>";
        }
		
        private void addHtmlTail()
        {
            Contents += _htmlTail;
        }
		
    }
}