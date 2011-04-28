using System.Collections.Generic;
using Driven.Metrics.Metrics;
using Driven.Metrics.Reporting;

namespace Driven.Metric.UI.Console
{
    public class ConsoleArguments   
    {
        public ConsoleArguments()
        {
            AssemblyNames = new List<string>();
            Metrics = new List<IMetric>();
        }
	        
        public bool Help {get;set;}
	
        public List<string> AssemblyNames { get; set; }
							
        public string ReportName { get; set; }

        public string XsltFilename { get; set; }
		
        public List<IMetric> Metrics { get; set; }
    }
}