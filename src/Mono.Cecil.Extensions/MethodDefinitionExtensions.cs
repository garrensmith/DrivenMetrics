using System.Linq;
using System.Text;

namespace Mono.Cecil.Extensions
{
    public static class MethodDefinitionExtensions
    {
        public static string FriendlyName(this MethodDefinition methodDefinition)
        {
            var sb = new StringBuilder ();
            sb.Append (methodDefinition.ReturnType.ReturnType);
            sb.Append (' ');
            sb.Append (methodDefinition.Name);
            sb.Append ('(');
            var e = methodDefinition.Parameters.Cast<ParameterDefinition>().GetEnumerator ();
            if (e.MoveNext ())
            {
                sb.Append (e.Current.ParameterType);
                while (e.MoveNext ())
                {
                    sb.Append (", ");
                    sb.Append (e.Current.ParameterType);
                }
            }
            sb.Append (')');
            return sb.ToString ();
        }
    }
}
