using System.Collections.Generic;
using Mono.Cecil;

namespace DrivenMetrics.Interfaces
{
    public interface IAssemblySearcher
    {
        MethodDefinition Find(string methodName);
        IEnumerable<TypeDefinition> GetAllTypes();
    }
}