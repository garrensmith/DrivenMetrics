using System.Collections.Generic;
using Mono.Cecil;

namespace DrivenMetrics.Tests.TestBuilders
{
    public class TypeDefinitionListBuilder
    {
        private TypeDefinitionBuilder _typeTypeDefinitionBuilder;

        public TypeDefinitionListBuilder()
        {
            _typeTypeDefinitionBuilder = new TypeDefinitionBuilder();
        }
        
        public IEnumerable<TypeDefinition> CreateTypeDefinitionList()
        {
            var type1 = _typeTypeDefinitionBuilder.CreateTypeDefinition();
            var type2 = _typeTypeDefinitionBuilder.CreateTypeDefinition();

            return new List<TypeDefinition> {type1, type2};
        }

    }
}
