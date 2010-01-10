using Mono.Cecil;

namespace DrivenMetrics.Tests.TestBuilders
{
    public class TypeDefinitionBuilder
    {
        public TypeDefinition CreateTypeDefinition()
        {
            return new TypeDefinition("test","test", new TypeAttributes(), null);
        }
    }
}
