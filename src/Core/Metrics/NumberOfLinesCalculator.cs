using System;
using System.Collections.Generic;
using Driven.Metrics.Metrics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Extensions;

namespace Driven.Metrics.metrics
{
    public class NumberOfLinesCalculator : IMetricCalculator
    {
        public int MaxPassValue {get; private set;}
        private List<int> _lineNumberCounted;

       public NumberOfLinesCalculator(int maxLines)
       {
           MaxPassValue = maxLines;
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

               if(results.Count == 0)
                   continue;

               classResults.Add(new ClassResult(typeDefinition.Name, results));
           }

           return new MetricResult("Number Of Lines Of Code", classResults);
       }

       public MethodResult Calculate(MethodDefinition methodDefinition)
        {
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
           var pass = isLessThanRecommended(lines);

           var friendlyName = methodDefinition.FriendlyName ();
           return new MethodResult(friendlyName, lines, pass);
        }

        private bool isLessThanRecommended(int lines)
        {
            if (lines > MaxPassValue)
                return false;

            return true;
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

        
    }
}
