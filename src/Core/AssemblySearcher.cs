using Driven.Metrics.Interfaces;
using Mono.Cecil;
using System.Collections.Generic;
using Mono.Cecil.Extensions;

namespace Driven.Metrics
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
        
        public IEnumerable<AssemblyDefinition> GetAllAssemblies()
        {
            foreach (AssemblyDefinition definition in _assemblyDefinitions)
            {
                yield return definition;
            }
        }
    }
}