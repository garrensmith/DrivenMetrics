using System.Collections.Generic;
using Driven.Metrics.Metrics;
using Mono.Cecil;

namespace Driven.Metrics.Metrics
{
    public interface IMetricCalculator
    {
		MetricResult Calculate(IEnumerable<AssemblyDefinition> assemblies, IMetric metric);
    }
}