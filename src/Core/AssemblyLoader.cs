using Driven.Metrics.Interfaces;
using Mono.Cecil;

namespace Driven.Metrics
{
    public class AssemblyLoader : IAssemlyLoader
    {
        private string _assemblyName;
        
        public AssemblyLoader(string assemblyName)
        {
            _assemblyName = assemblyName;
        }

        public AssemblyDefinition Load()
        {
            var assemblyDef = AssemblyFactory.GetAssembly(_assemblyName);
			assemblyDef.MainModule.LoadSymbols();

            return assemblyDef;
        }


    }
}