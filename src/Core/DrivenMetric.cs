using System.Collections.Generic;
using DrivenMetrics.Interfaces;
using Mono.Cecil;
using DrivenMetrics.Reporting;
using DrivenMetrics.metrics;
using DrivenMetrics.Metrics;
using System;

namespace DrivenMetrics
{
    public class DrivenMetric
    {
        public readonly IAssemblySearcher _assemblySearcher;
        public readonly IReport Report;
        public readonly IMetricCalculator[] _metricCalculators;

        public DrivenMetric(IAssemblySearcher methodFinder, IReport report, IMetricCalculator[] metricCalculators )
        {
            
            _assemblySearcher = methodFinder;
            Report = report;
            _metricCalculators = metricCalculators;
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

        public DrivenMetric Create(string[] assemblyNames)
            {
                throw new Exception();
            }

        public void RunAllMetricsAndGenerateReport()
        {
            var metricResults = new List<MetricResult>();

            foreach (var calculator in _metricCalculators)
            {
                var result = calculator.Calculate(_assemblySearcher.GetAllTypes());
                metricResults.Add(result);
            }

            Report.Generate(metricResults);
        }
        
        
        public class Factory
        {
            public DrivenMetric Create(string assemblyName,  string reportFilePath)
            {
                return Create(new[] {assemblyName}, reportFilePath);
            }
			
			public DrivenMetric Create(string[] assemblyNames, string reportFilePath)
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
                var htmlReport = new HtmlFailedReport(new FileWriter(), reportFilePath);
                var numberOfLines = new NumberOfLinesCalculator(20);
                var cyclomicCompexity = new ILCyclomicComplextityCalculator(20);

                var drivenMetric = new DrivenMetric(methodFinder, htmlReport, new IMetricCalculator[] { numberOfLines, cyclomicCompexity });

                return drivenMetric;
            }
			
			public DrivenMetric Create(string[] assemblyNames, IMetricCalculator[] metrics, string reportFilePath, IReport htmlReport)
			{
				 var assemblies = new List<AssemblyDefinition>();

                foreach (var assemblyName in assemblyNames)
                {
                    var assemblyLoader = new AssemblyLoader(assemblyName);
                    var assembly = assemblyLoader.Load();    
                    assemblies.Add(assembly);
                }

                var methodFinder = new AssemblySearcher(assemblies.ToArray());
				var drivenMetric = new DrivenMetric(methodFinder, htmlReport, metrics);

                return drivenMetric;	
				
			}
        }
    }
}
