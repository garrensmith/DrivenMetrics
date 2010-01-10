using DrivenMetrics.Interfaces;
using Mono.Cecil;

namespace DrivenMetrics
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