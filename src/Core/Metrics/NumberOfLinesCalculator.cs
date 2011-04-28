using System;
using System.Collections.Generic;
using Driven.Metrics.Metrics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Extensions;
using System.Linq;

namespace Driven.Metrics.Metrics
{
    public class NumberOfLinesMetric : IMetric
    {
        private List<int> _lineNumberCounted;

        public NumberOfLinesMetric()
        {
        }

        private MethodResult Calculate(MethodDefinition methodDefinition)
        {
            var friendlyName = methodDefinition.FriendlyName();
            if (methodDefinition.Body == null)
                return new MethodResult(friendlyName, 0);

            _lineNumberCounted = new List<int>();

            foreach (Instruction ins in methodDefinition.Body.Instructions.WithSequencePoint())
            {
                if (IsBracketOrReturnOpCode(ins.OpCode))
                    continue;

                if (hasLineBeenCounted(ins.SequencePoint.StartLine))
                    continue;

                addLine(ins.SequencePoint.StartLine);
            }

            var lines = getLineCount();
            
            return new MethodResult(friendlyName, lines);
        }

        private void addLine(int startline)
        {
            _lineNumberCounted.Add(startline);
        }

        private int getLineCount()
        {
            return _lineNumberCounted.Count == 0 ? 1 : _lineNumberCounted.Count;
        }

        public bool hasLineBeenCounted(int startline)
        {
            // if (startline == 0xFeeFee)
            //     return true;

            if (_lineNumberCounted.Contains(startline))
                return true;

            return false;
        }

        private bool IsBracketOrReturnOpCode(OpCode opCode)
        {
            if (opCode.Code == Code.Nop)
                return true;

            if (opCode.Code == Code.Ret)
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
            MetricResult result = new MetricResult("Lines of code");
            result.AssemblyResults = assemblyResults;
            result.Result = assemblyResults.Sum(r => r.Result);
            return result;
        }
    }
}
