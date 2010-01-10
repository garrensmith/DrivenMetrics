using System.Collections.Generic;
using DrivenMetrics.Metrics;

namespace DrivenMetrics.Reporting
{
    public interface IReport
    {
        void Generate(IEnumerable<MetricResult> results);
        void Generate(MetricResult result);
    }
}