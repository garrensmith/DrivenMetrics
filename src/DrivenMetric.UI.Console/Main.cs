
using System;
using System.Collections.Generic;
using DrivenMetrics.metrics;
using DrivenMetrics.Reporting;

namespace DrivenMetrics.UI.Console
{
	
    public static class Program
	{
		[STAThread]
        public static void Main (string[] args)
		{
		    var argumentparser = new ConsoleArgumentParser(args);

		    var argument = argumentparser.Parse();
            
            if (argument.Help)
			{
				displayHelpCommands();
				return;
			}
			
			
			var drivenMetric = bootStrap(argument);
			
			drivenMetric.RunAllMetricsAndGenerateReport();
		}

		static void displayHelpCommands ()
		{
			System.Console.WriteLine("Driven Metric Code Analyzer");
			System.Console.WriteLine();
			System.Console.WriteLine("/? -- Help");
			System.Console.WriteLine("-a -- assemblies to analyze eg test.dll test2.dll test3.exe");
			System.Console.WriteLine("-rAll reportname.html -- Create report for all methods");
			System.Console.WriteLine("-rFail reportname.html -- Create report for all failing methods");
			System.Console.WriteLine("-loc n -- Calculate lines of code for each method, n = failing value");
			System.Console.WriteLine("-cc n -- Calculate Cyclomic Complexity of assemblies, n = failing value");
			System.Console.WriteLine();
			System.Console.WriteLine("Example DrivenMetric.UI.Console -a test.dll test2.dll -cc -loc 15 -r output.html");
		}


		private static DrivenMetric bootStrap (DrivenMetrics.UI.Console.ConsoleArguments argument)
		{
			var reportFactory = new ReportFactory();
			
			var htmlReport = reportFactory.ResolveReport(argument.ReportType, argument.ReportName);
			
			return new DrivenMetric.Factory().Create(argument.AssemblyNames.ToArray(),
			                                         argument.Metrics.ToArray(), 
			                                         argument.ReportName, htmlReport);	
		}

	}    
}
