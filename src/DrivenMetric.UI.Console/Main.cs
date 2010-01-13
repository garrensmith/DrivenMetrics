using System;
using DrivenMetrics.Reporting;
using NDesk.Options;
using DrivenMetrics.metrics;

namespace DrivenMetric.UI.Console
{
    public static class Program
    {
        /*[STAThread]
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
        }*/
        
        [STAThread]
        public static void Main(string[] args)
        {
            var consoleArgument = new ConsoleArguments();
            
            var optionSet = new OptionSet() {
                                        { "a|assembly=", "the {assembly} to analyze",
                                            v => consoleArgument.AssemblyNames.Add(v) },
                                        { "cc=", 
                                            "Calculate Cyclomic Complexity with {maximum} acceptable complexity",
                                            (int v) => consoleArgument.Metrics.Add(new ILCyclomicComplextityCalculator(v)) },
                                        { "loc=", "Calculate Lines of Code Metric with {maximum} lines of code",
                                            (int v) => consoleArgument.Metrics.Add(new NumberOfLinesCalculator(v))
                                            },
                                        {"rAll=", "Generate report for all methods",v =>
                                                                                       {
                                                                                           consoleArgument.
                                                                                               ReportType =
                                                                                               ReportType.All;
                                                                                           consoleArgument.
                                                                                               ReportName = v;
                                                                                       }},
                                       {"rFail=", "Generate report for all failing methods",v =>
                                       {
                                           consoleArgument.
                                               ReportType =
                                               ReportType.Failing;
                                           consoleArgument.
                                               ReportName = v;
                                       }},

                                        { "h|help",  "show this message and exit", 
                                            v => consoleArgument.Help = v != null },
                                    };

            try
            {
                optionSet.Parse(args);
            }
            catch (OptionException e)
            {
                System.Console.Write("bundling: ");
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Try `--help' for more information.");
                return;
            }

            if (consoleArgument.Help)
            {
                ShowHelp(optionSet);
                return;
            }

            var drivenMetric = bootStrap(consoleArgument);
            drivenMetric.RunAllMetricsAndGenerateReport();
        }

        static void ShowHelp(OptionSet p)
        {
            System.Console.WriteLine("Driven Metric Code Analyzer");
            System.Console.WriteLine("Usage: bundling [OPTIONS]+");
            System.Console.WriteLine();
            System.Console.WriteLine("Options:");
            p.WriteOptionDescriptions(System.Console.Out);
            System.Console.WriteLine();
            System.Console.WriteLine("Example:");
            System.Console.WriteLine("DrivenMetric.UI.Console -a test.dll -a test2.dll -cc -loc 15 -rFail output.html");
        }

        private static DrivenMetrics.DrivenMetric bootStrap(ConsoleArguments argument)
        {
            var reportFactory = new ReportFactory();

            var htmlReport = reportFactory.ResolveReport(argument.ReportType, argument.ReportName);

            return new DrivenMetrics.DrivenMetric.Factory().Create(argument.AssemblyNames.ToArray(),
                                                     argument.Metrics.ToArray(),
                                                     argument.ReportName, htmlReport);
        }

    }
}