using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil.Cil;
using Mono.Cecil;
using System.Collections;
using Mono.Cecil.Extensions;

namespace Driven.Metrics.Metrics
{
    public class CyclomaticComplexityReflectorMetric:IMetric
    {
        CyclomaticComplexityAlgorithm algorithm = new CyclomaticComplexityAlgorithm();

        public MethodResult ProcessMethod(MethodDefinition methodDefinition)
        {
            MethodResult methodResult = new MethodResult();
            methodResult.MethodName = methodDefinition.FriendlyName();
            methodResult.Result = algorithm.Compute(methodDefinition.Body);
            return methodResult;
        }

        public TypeResult ProcessType(TypeDefinition type, MethodResult[] methodResults)
        {
            TypeResult result = new TypeResult(type.Name);
            result.MethodResults = methodResults;
            result.Result = methodResults.Where(r => r.Result != -1).Sum(r => r.Result);
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
            MetricResult result = new MetricResult("Cyclomatic complexity from reflector");
            result.AssemblyResults = assemblyResults;
            result.Result = assemblyResults.Sum(r => r.Result);
            return result;
        }
    }

    internal sealed class CyclomaticComplexityAlgorithm
    {
        private FlowToCodeConverter flowConverter = new FlowToCodeConverter();

        public int Compute(MethodBody body)
        {
            int cyclo = 0;
            foreach (Instruction instruction in body.Instructions)
            {
                System.Reflection.Emit.FlowControl flow =
                    this.flowConverter.Convert((int)instruction.OpCode.Code);
                if (flow == System.Reflection.Emit.FlowControl.Cond_Branch)
                    cyclo++;
            }

            return cyclo + 1;
        }

        public int Compute(TypeDefinition type)
        {
            int cyclo = 0;
            foreach (MethodDefinition method in type.Methods)
            {
                MethodBody body = method.Body;
                if (body == null)
                    return -1;
                cyclo += Compute(body);
            }
            return cyclo;
        }

        private sealed class FlowToCodeConverter
        {
            private Hashtable codeFlows = new Hashtable();
            public FlowToCodeConverter()
            {
                foreach (System.Reflection.FieldInfo fi in typeof(System.Reflection.Emit.OpCodes).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
                {
                    System.Reflection.Emit.OpCode code = (System.Reflection.Emit.OpCode)fi.GetValue(null);
                    this.codeFlows[(int)code.Value] = code.FlowControl;
                }
            }

            public System.Reflection.Emit.FlowControl Convert(int code)
            {
                Object o = this.codeFlows[code];
                if (o == null)
                {
                    return System.Reflection.Emit.FlowControl.Meta;
                }

                //	throw new Exception(String.Format("code.Value {0} not found",code.Value));

                return (System.Reflection.Emit.FlowControl)o;
            }
        }
    }
}
