using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;

namespace Driven.Metrics.Metrics
{
    public class MetricCalculator : IMetricCalculator
    {
        public MetricResult Calculate(IEnumerable<AssemblyDefinition> assemblies, IMetric metric)
        {
            List<AssemblyResult> assemblyResults = new List<AssemblyResult>();
            foreach(AssemblyDefinition assembly in assemblies)
            {
                List<ModuleResult> moduleResults = new List<ModuleResult>();
                foreach (ModuleDefinition module in assembly.Modules)
                {
                    List<TypeResult> typeResults = new List<TypeResult>();
                    foreach (TypeDefinition type in module.Types)
                    {
                        List<MethodResult> methodResults = new List<MethodResult>();
                        foreach (MethodDefinition method in type.Methods)
                        {
                            MethodResult methodResult = metric.ProcessMethod(method);
                            methodResults.Add(methodResult);
                        }
                        TypeResult typeResult = metric.ProcessType(type, methodResults.ToArray());
                        typeResults.Add(typeResult);
                    }
                    ModuleResult moduleResult = metric.ProcessModule(module, typeResults.ToArray());
                    moduleResults.Add(moduleResult);
                }
                AssemblyResult assemblyResult = metric.ProcessAssembly(assembly, moduleResults.ToArray());
                assemblyResults.Add(assemblyResult);
            }
            MetricResult result = metric.Process(assemblyResults.ToArray());
            return result;
        }


        
    }
}
