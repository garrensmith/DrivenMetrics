namespace Mono.Cecil.Extensions
{
    public static class TypeDefinitionExtentsions
    {

        public static bool IsValidForMetrics(this TypeDefinition typeDefinition)
        {
            if (typeDefinition.Name == "<Module>")
                return false;

            if (typeDefinition.Name.StartsWith("<PrivateImplementationDetails>"))
                return false;

            //need to write a test for this
            if (typeDefinition.Name.Contains("__"))
                return false;
            
            if (!typeDefinition.IsClass)
                return false;

            
            return true;
        }
    }
}
