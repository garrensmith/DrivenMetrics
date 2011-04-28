using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Driven.Metrics.Metrics;

namespace Driven.Metrics.Reporting
{
    [Serializable]
    public class ReportResult
    {
        public MetricResult[] MetricResults
        {
            get;
            set;
        }

        public ReportResult()
        {
        }
    }
}
