using System.Collections.Generic;
using Driven.Metrics.Metrics;
using Mono.Cecil.Extensions;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Linq;


namespace Driven.Metrics.Metrics
{
    public class ILCyclomicComplextityMetric : IMetric
    {
        public ILCyclomicComplextityMetric()
        {
        }

        private MethodResult Calculate(MethodDefinition methodDefinition)
        {
            var friendlyName = methodDefinition.FriendlyName();
            if (methodDefinition.Body == null)
                return new MethodResult(friendlyName, 0);

            int cc = 1;

            foreach (Instruction instruction in methodDefinition.Body.Instructions)
            {
                if (isAnotherPath(instruction.OpCode))
                    cc++;
            }
            
            return new MethodResult(friendlyName, cc);
        }

        private bool isAnotherPath(OpCode opCode)
        {
            // if (opCode.Code == Code.Switch)
            //     return true;

            if (opCode.FlowControl == FlowControl.Cond_Branch)
                return true;

            if (opCode.FlowControl == FlowControl.Branch)
                return true;

            if (opCode.Code == Code.Jmp)
                return true;

            return false;
        }


        public MethodResult ProcessMethod(MethodDefinition method)
        {
            return Calculate(method);
        }

        public TypeResult ProcessType(TypeDefinition type, MethodResult[] methodResults)
        {
            TypeResult result = new TypeResult(type.Name);
            result.MethodResults = methodResults;
            result.Result = methodResults.Sum(r => r.Result);
            return result;
        }

        public ModuleResult ProcessModule(ModuleDefinition module, TypeResult[] typeResults)
        {
            ModuleResult result = new ModuleResult(module.Name);
            result.TypeResults = typeResults;
            result.Result = typeResults.Sum(r => r.Result);
            return result;
        }

        public AssemblyResult ProcessAssembly(AssemblyDefinition assembly, ModuleResult[] moduleResults)
        {
            AssemblyResult result = new AssemblyResult(assembly.Name.Name);
            result.ModuleResults = moduleResults;
            result.Result = moduleResults.Sum(r => r.Result);
            return result;
        }

        public MetricResult Process(AssemblyResult[] assemblyResults)
        {
            MetricResult result = new MetricResult("Cyclomatic complexity");
            result.AssemblyResults = assemblyResults;
            result.Result = assemblyResults.Sum(r => r.Result);
            return result;
        }
    }
}