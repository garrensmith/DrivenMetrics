using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Driven.Metrics.Metrics
{
    [Serializable]
    public class TypeResult
    {
        [XmlAttribute]
        public string TypeName { get; set; }
        public MethodResult[] MethodResults { get; set; }
        [XmlAttribute]
        public int Result { get; set; }

        public TypeResult(string typeName)
        {
            TypeName = typeName;
        }

        public TypeResult()
        {
        }

    }
}
