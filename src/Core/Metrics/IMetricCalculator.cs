using System.Collections.Generic;
using DrivenMetrics.Metrics;
using Mono.Cecil;

namespace DrivenMetrics.metrics
{
    public interface IMetricCalculator
    {
        int MaxPassValue {get;}
		MetricResult Calculate(IEnumerable<TypeDefinition> types);
        MethodResult Calculate(MethodDefinition methodDefinition);
    }
}