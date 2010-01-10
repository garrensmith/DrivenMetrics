using Mono.Cecil;

namespace DrivenMetrics.Interfaces
{
    public interface IAssemlyLoader
    {
        AssemblyDefinition Load();
    }
}