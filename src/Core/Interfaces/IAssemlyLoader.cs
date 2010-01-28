using Mono.Cecil;

namespace Driven.Metrics.Interfaces
{
    public interface IAssemlyLoader
    {
        AssemblyDefinition Load();
    }
}