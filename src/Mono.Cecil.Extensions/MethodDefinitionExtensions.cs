namespace Mono.Cecil.Extensions
{
    public static class MethodDefinitionExtensions
    {
        public static string FriendlyName(this MethodDefinition methodDefinition)
        {
            return methodDefinition.Name;
        }
    }
}
