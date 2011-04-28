using System.Collections.Generic;
using Mono.Cecil;

namespace Driven.Metrics.Interfaces
{
    public interface IAssemblySearcher
    {
        IEnumerable<AssemblyDefinition> GetAllAssemblies();
    }
}