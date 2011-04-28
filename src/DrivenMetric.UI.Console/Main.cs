using System;
using Driven.Metrics;
using Driven.Metrics.Metrics;
using Driven.Metrics.Reporting;
using NDesk.Options;

namespace Driven.Metric.UI.Console
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var consoleArgument = new ConsoleArguments();

            var optionSet = new OptionSet() {
                                        { "a|assembly=", "the {assembly} to analyze",
                                            v => consoleArgument.AssemblyNames.Add(v) },
                                        { "cc=", 
                                            "Calculate Cyclomic Complexity with {maximum} acceptable complexity",
                                            v => consoleArgument.Metrics.Add(new ILCyclomicComplextityMetric()) },
                                        { "loc=", "Calculate Lines of Code Metric with {maximum} lines of code",
                                            v => consoleArgument.Metrics.Add(new NumberOfLinesMetric())
                                            },
                                        { "xslt=", "Xslt filename",
                                            filename => consoleArgument.XsltFilename = filename },
                                        { "r=", "Report filename",
                                            filename => consoleArgument.ReportName = filename }
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

            //try
            {
                var drivenMetric = bootStrap(consoleArgument);
                drivenMetric.RunAllMetricsAndGenerateReport();
            }
            //catch (Exception ex)
            //{
            //    System.Console.WriteLine("An error occured:");
            //    System.Console.WriteLine(ex.Message);
            //}
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
            System.Console.WriteLine("DrivenMetric.UI.Console -a test.dll -a test2.dll -a \"DrivenMetric.UI.Console.exe\" -cc -loc -r output.xml -xslt thesame.xslt");
        }

        private static DrivenMetrics bootStrap(ConsoleArguments argument)
        {
            //var reportFactory = new ReportFactory();

            //var htmlReport = reportFactory.ResolveReport(argument.ReportType, argument.ReportName);

            return new DrivenMetrics.Factory().Create(argument.AssemblyNames.ToArray(),
                                                     argument.Metrics.ToArray(),
                                                     argument.ReportName, new Report(argument.ReportName, argument.XsltFilename));
        }
    }
}