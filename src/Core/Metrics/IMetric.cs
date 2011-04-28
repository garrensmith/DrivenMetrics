using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;

namespace Driven.Metrics.Metrics
{
    public interface IMetric
    {
        MethodResult ProcessMethod(MethodDefinition method);

        TypeResult ProcessType(TypeDefinition type, MethodResult[] methodResults);

        ModuleResult ProcessModule(ModuleDefinition module, TypeResult[] typeResults);

        AssemblyResult ProcessAssembly(AssemblyDefinition assembly, ModuleResult[] moduleResults);

        MetricResult Process(AssemblyResult[] assemblyResults);
    }
}
