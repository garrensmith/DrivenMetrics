using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Driven.Metrics.Metrics
{
    [Serializable]
    public class AssemblyResult
    {
        [XmlAttribute]
        public string AssemblyName { get; set; }
        [XmlAttribute]
        public int Result { get; set; }
        public ModuleResult[] ModuleResults {get; set;}

        public AssemblyResult(string assemblyName)
        {
            AssemblyName = assemblyName;
        }

        public AssemblyResult()
        {
        }
    }
}
