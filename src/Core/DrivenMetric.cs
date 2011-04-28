using System.Collections.Generic;
using Driven.Metrics.Interfaces;
using Mono.Cecil;
using Driven.Metrics.Reporting;
using Driven.Metrics.Metrics;
using System;

namespace Driven.Metrics
{
    public class DrivenMetrics
    {
        public readonly IAssemblySearcher _assemblySearcher;
        public readonly IReport Report;
        public readonly IMetric[] _metrics;

        public DrivenMetrics(IAssemblySearcher methodFinder, IReport report, IMetric[] metrics)
        {

            _assemblySearcher = methodFinder;
            Report = report;
            _metrics = metrics;
        }

        /*    public void CalculateLinesOfCode()
        {
            foreach (TypeDefinition typeInAsm in _assemblyDefinition.MainModule.Types)
            {
                if (!typeInAsm.IsClass)
                    continue;
                
                Console.WriteLine("Type: " + typeInAsm);

                foreach (MethodDefinition method in typeInAsm.Methods)
                {
                    if (method.IsConstructor || method.IsSetter || method.IsGetter || method.IsAbstract)
                        continue;
                }
            }
        }
      

        public int CalculateCyclomicComplexity(string methodName)
        {
            var methodDefinition = getMethod(methodName);
            
            var cyclomicCompexity = new ILCyclomicComplextityCalculator();
            return cyclomicCompexity.Calculate(methodDefinition);
            return 5;
        }*/

        public DrivenMetrics Create(string[] assemblyNames)
        {
            throw new Exception();
        }

        public void RunAllMetricsAndGenerateReport()
        {
            var metricResults = new List<MetricResult>();

            foreach (var metric in _metrics)
            {
                IMetricCalculator calculator = new MetricCalculator();

                var result = calculator.Calculate(_assemblySearcher.GetAllAssemblies(), metric);
                metricResults.Add(result);
            }

            Report.Generate(metricResults.ToArray());
        }


        public class Factory
        {
            public DrivenMetrics Create(string assemblyName, string reportFilePath)
            {
                return Create(new[] { assemblyName }, reportFilePath);
            }

            public DrivenMetrics Create(string[] assemblyNames, string reportFilePath)
            {
                var assemblies = new List<AssemblyDefinition>();

                foreach (var assemblyName in assemblyNames)
                {
                    var assemblyLoader = new AssemblyLoader(assemblyName);
                    var assembly = assemblyLoader.Load();
                    assemblies.Add(assembly);
                }

                var methodFinder = new AssemblySearcher(assemblies.ToArray());
                //var htmlReport = new HtmlReport(new FileWriter(), reportFilePath);
                //var htmlReport = new HtmlFailedReport(new FileWriter(), reportFilePath);
                var numberOfLines = new NumberOfLinesMetric();
                var cyclomicCompexity = new ILCyclomicComplextityMetric();

                var drivenMetric = new DrivenMetrics(methodFinder, new Report(), new IMetric[] { numberOfLines, cyclomicCompexity });

                return drivenMetric;
            }

            public DrivenMetrics Create(string[] assemblyNames, IMetric[] metrics, string reportFilePath, IReport htmlReport)
            {
                var assemblies = new List<AssemblyDefinition>();

                foreach (var assemblyName in assemblyNames)
                {
                    var assemblyLoader = new AssemblyLoader(assemblyName);
                    var assembly = assemblyLoader.Load();
                    assemblies.Add(assembly);
                }

                var methodFinder = new AssemblySearcher(assemblies.ToArray());
                var drivenMetric = new DrivenMetrics(methodFinder, htmlReport, metrics);

                return drivenMetric;

            }
        }
    }
}
