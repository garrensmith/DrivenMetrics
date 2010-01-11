using System;
using DrivenMetrics.Reporting;
using NDesk.Options;

namespace DrivenMetric.UI.Console
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


        private static DrivenMetrics.DrivenMetric bootStrap (ConsoleArguments argument)
        {
            var reportFactory = new ReportFactory();
			
            var htmlReport = reportFactory.ResolveReport(argument.ReportType, argument.ReportName);
			
            return new DrivenMetrics.DrivenMetric.Factory().Create(argument.AssemblyNames.ToArray(),
                                                     argument.Metrics.ToArray(), 
                                                     argument.ReportName, htmlReport);	
        }

       /* private static OptionSet ConsoleOptionsBootStrap()
        {
            var optionSet = new OptionSet() {
                                        { "a|assembly=", "the {assembly} of someone to greet.",
                                            v => assemblies.Add (v) },
                                        { "r|repeat=", 
                                            "the number of {TIMES} to repeat the greeting.\n" + 
                                            "this must be an integer.",
                                            (int v) => repeat = v },
                                        { "v", "increase debug message verbosity",
                                            v => { if (v != null) ++verbosity; } },
                                        { "h|help",  "show this message and exit", 
                                            v => show_help = v != null },
                                    };
            
            return optionSet;
        }*/

    }
}