using System.Collections.Generic;
using DrivenMetrics.Metrics;

namespace DrivenMetrics.Tests.TestBuilders
{
    public class MetricResultBuilder
    {
        private MethodResult _methodResult1;
        private MethodResult _methodResult2;
        private ClassResult _classResult1;
        private ClassResult _classResult2;
        private MetricResult _metricResult;
		
        public MetricResultBuilder()
        {
            _methodResult1 = new MethodResult("Test Method1",12,false);
            _methodResult2 = new MethodResult("Test Method2",5,true);
            _classResult1 = new ClassResult("Test Class 1", new List<MethodResult> {_methodResult1,_methodResult2}); 
            _classResult2 = new ClassResult("Test Class 1", new List<MethodResult> {_methodResult1,_methodResult2}); 
            _metricResult = new MetricResult("Test Metric", new List<ClassResult> {_classResult1,_classResult2});
        }
		
        public MetricResult CreateMetricResult()
        {
            return _metricResult;
        }
		
    }
}