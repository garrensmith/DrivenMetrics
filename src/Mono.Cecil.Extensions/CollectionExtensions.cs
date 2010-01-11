using System.Collections.Generic;
using System.Linq;
using Mono.Cecil.Cil;

namespace Mono.Cecil.Extensions
{
    public static class InstructionCollectionExtensions
    {
        public static IEnumerable<Instruction> WithSequencePoint(this InstructionCollection instructionCollection)
        {
            return from Instruction instruction in instructionCollection
                   where instruction.SequencePoint != null
                   orderby instruction.SequencePoint.StartLine
                   select instruction;
        }

        public static IEnumerable<MethodDefinition> WithBodys(this MethodDefinitionCollection methodDefinitionCollection)
        {
            return from MethodDefinition method in methodDefinitionCollection
                   where method.Body != null && method.IsSetter == false && method.IsGetter == false && method.IsConstructor == false && method.Name.Contains("__") == false
                   select method;
        }
    }
}

