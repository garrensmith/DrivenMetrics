using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Driven.Metrics.Metrics
{
    [Serializable]
    public class MethodResult
    {
        [XmlAttribute]
        public string MethodName { get; set; }
        [XmlAttribute]
        public int Result { get; set; }

        public MethodResult(string methodName, int result)
        {
            MethodName = methodName;
            Result = result;
        }

        public MethodResult()
        {
        }

    }
}
