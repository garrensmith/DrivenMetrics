using System.Collections.Generic;
using Driven.Metrics.Metrics;

namespace Driven.Metrics.Reporting
{
    public interface IReport
    {
        string Generate(params MetricResult[] metricResults);
    }
}