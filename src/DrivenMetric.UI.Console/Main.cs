using System;
using Driven.Metrics;
using Driven.Metrics.Reporting;
using NDesk.Options;
using Driven.Metrics.metrics;

namespace Driven.Metric.UI.Console
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            //var serviceLocator = new ServiceLocator();
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
                                       {"rTopTen=", "Generate report for all failing methods",v =>
                                       {
                                           consoleArgument.ReportType =
                                               ReportType.TopTen;
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

            try
            {
                var drivenMetric = bootStrap(consoleArgument);
                drivenMetric.RunAllMetricsAndGenerateReport();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("An error occured:");
                System.Console.WriteLine(ex.Message);
            }
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

        private static DrivenMetrics bootStrap(ConsoleArguments argument)
        {
            var reportFactory = new ReportFactory();

            var htmlReport = reportFactory.ResolveReport(argument.ReportType, argument.ReportName);

            return new DrivenMetrics.Factory().Create(argument.AssemblyNames.ToArray(),
                                                     argument.Metrics.ToArray(),
                                                     argument.ReportName, htmlReport);
        }
    }
}