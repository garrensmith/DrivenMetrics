using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mono.Cecil.Extensions
{
    public static class TypeDefinitionExtentsions
    {

        public static bool IsValidForMetrics(this TypeDefinition typeDefinition)
        {
            if (typeDefinition.Name == "<Module>")
                return false;

            if (!typeDefinition.IsClass)
                return false;

            return true;
        }
    }
}
