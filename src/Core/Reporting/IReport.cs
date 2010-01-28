using System.Collections.Generic;
using Driven.Metrics.Metrics;

namespace Driven.Metrics.Reporting
{
    public interface IReport
    {
        void Generate(IEnumerable<MetricResult> results);
        void Generate(MetricResult result);
    }
}