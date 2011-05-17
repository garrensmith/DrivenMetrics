using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Driven.Metrics.Metrics
{
    [Serializable]
    public class ModuleResult
    {
        [XmlAttribute]
        public string ModuleName { get; set; }
        public TypeResult[] TypeResults {get; set;}
        [XmlAttribute]
        public int Result { get; set; }

        public ModuleResult(string moduleName)
        {
            ModuleName = moduleName;
        }

        public ModuleResult()
        {
        }
    }
}
