using Mono.Cecil;

namespace Driven.Metrics.Tests.TestBuilders
{
    public class TypeDefinitionBuilder
    {
        public TypeDefinition CreateTypeDefinition()
        {
            return new TypeDefinition("test","test", new TypeAttributes(), null);
        }
    }
}
