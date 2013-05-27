using System.Collections.Generic;
using System;
using System.Xml.Serialization;

namespace Driven.Metrics.Metrics
{
    [Serializable]
    public class MetricResult
    {
        [XmlAttribute]
        public string MetricName {get; set;}
        public AssemblyResult[] AssemblyResults {get; set;}
        [XmlAttribute]
        public int Result { get; set; }

        public MetricResult(string metricName)
        {
            MetricName = metricName;
        }

        public MetricResult()
        {
        }
    }
}