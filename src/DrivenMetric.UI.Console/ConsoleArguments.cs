
using System;
using System.Collections.Generic;
using DrivenMetrics.metrics;
using DrivenMetrics.Reporting;

namespace DrivenMetrics.UI.Console
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
