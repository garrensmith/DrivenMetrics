using System.Collections.Generic;
using Driven.Metrics.metrics;
using Driven.Metrics.Reporting;

namespace Driven.Metric.UI.Console
{
    public class ConsoleArguments   
    {
        public ConsoleArguments()
        {
            AssemblyNames = new List<string>();
            Metrics = new List<IMetricCalculator>();
        }
	        
        public bool Help {get;set;}
	
        public List<string> AssemblyNames { get; set; }
							
        public string ReportName { get; set; }
		
        public ReportType ReportType {get; set;}
			
        public List<IMetricCalculator> Metrics { get; set; }
    }
}