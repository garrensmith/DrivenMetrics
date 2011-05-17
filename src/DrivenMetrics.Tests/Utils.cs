using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;

namespace DrivenMetrics.Tests
{
    public static class Utils
    {
        public static MethodDefinition GetMethod(AssemblyDefinition assemblyDefinition, string fullTypeName, string methodName)
        {
            TypeDefinition type = GetType(assemblyDefinition, fullTypeName);
            foreach (MethodDefinition method in type.Methods)
                if (method.Name == methodName)
                    return method;
            return null;
        }

        public static TypeDefinition GetType(AssemblyDefinition assemblyDefinition, string fullTypeName)
        {
            foreach (ModuleDefinition module in assemblyDefinition.Modules)
            {
                foreach (TypeDefinition type in module.Types)
                {
                    if (type.FullName == fullTypeName)
                        return type;
                }
            }
            return null;
        }
    }
}
