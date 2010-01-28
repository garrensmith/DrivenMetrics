using System.Collections.Generic;
using Driven.Metrics.Metrics;
using Mono.Cecil;

namespace Driven.Metrics.metrics
{
    public interface IMetricCalculator
    {
        int MaxPassValue {get;}
		MetricResult Calculate(IEnumerable<TypeDefinition> types);
        MethodResult Calculate(MethodDefinition methodDefinition);
    }
}