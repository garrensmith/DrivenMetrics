using System.Collections.Generic;
using Mono.Cecil;

namespace Driven.Metrics.Interfaces
{
    public interface IAssemblySearcher
    {
        MethodDefinition FindMethod(string methodName);
        IEnumerable<TypeDefinition> GetAllTypes();
    }
}