using System;
using System.Collections.Generic;
using DrivenMetrics.Metrics;
using Mono.Cecil.Extensions;
using Mono.Cecil;
using Mono.Cecil.Cil;


namespace DrivenMetrics.metrics
{
    public class CyclomicComplextityCalculator : IMetricCalculator
    {
        	
		public int MaxPassValue {get; private set;}

        public CyclomicComplextityCalculator(int maxPassValue)
        {
            MaxPassValue = maxPassValue;
        }

        public MethodResult Calculate(MethodDefinition methodDefinition)
        {
            int cc = 1;

            foreach (Instruction instruction in methodDefinition.Body.Instructions)
            {
                if (isAnotherPath(instruction.OpCode))
                    cc++;
            }

            return new MethodResult(methodDefinition.Name,cc, isAcceptableComplexity(cc));
        }

        private bool isAcceptableComplexity(int cc)
        {
            if (cc > MaxPassValue)
                return false;

            return true;
        }

        private bool isAnotherPath(OpCode opCode)
        {
            if (opCode.FlowControl == FlowControl.Branch)
                return true;

            if (opCode.Code == Code.Jmp)
                return true;

            return false;

            
        }

        public MetricResult Calculate(IEnumerable<TypeDefinition> types)
        {
            var classResults = new List<ClassResult>();

            foreach (TypeDefinition typeDefinition in types)
            {
                var results = new List<MethodResult>();

                foreach (MethodDefinition method in typeDefinition.Methods.WithBodys())
                {
                    var methodResult = Calculate(method);
                    results.Add(methodResult);
                }

                if (results.Count == 0)
                    continue;

                classResults.Add(new ClassResult(typeDefinition.Name, results));
            }

            return new MetricResult("Cyclomic Complexity", classResults);
        }
    }
}