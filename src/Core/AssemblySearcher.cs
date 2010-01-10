using DrivenMetrics.Interfaces;
using Mono.Cecil;
using System.Collections.Generic;
using Mono.Cecil.Extensions;

namespace DrivenMetrics
{
    public class AssemblySearcher : IAssemblySearcher
    {
        private readonly AssemblyDefinition[] _assemblyDefinitions;

        public AssemblySearcher(AssemblyDefinition assemblyDefinition): this(new[] {assemblyDefinition})
        {
            
        }

        public AssemblySearcher(AssemblyDefinition[] assemblyDefinitions)
        {
            _assemblyDefinitions = assemblyDefinitions;
        }
        
        //TODO: test for polymorphism
        public MethodDefinition Find(string methodName)
        {
            foreach (AssemblyDefinition definition in _assemblyDefinitions)
            {
                foreach (TypeDefinition type in definition.MainModule.Types)
                {
                    foreach (MethodDefinition method in type.Methods)
                    {
                        if (method.Name == methodName)
                        {
                            return method;
                        }
                    }
                }
            }

            return null;
        }

        public IEnumerable<TypeDefinition> GetAllTypes()
        {
            foreach (AssemblyDefinition definition in _assemblyDefinitions)
            {
                foreach (TypeDefinition type in definition.MainModule.Types)
                {
                    if (type.IsValidForMetrics())
                        yield return type;
                }
            }
        }
    }
}