using System.Collections.Generic;
using Driven.Metrics.Metrics;

namespace Driven.Metrics.Reporting
{
    public interface IReport
    {
        void Generate(params MetricResult[] metricResults);
    }
}