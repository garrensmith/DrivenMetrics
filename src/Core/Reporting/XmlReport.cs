using System;
using System.Collections.Generic;
using Driven.Metrics.Metrics;

namespace Driven.Metrics.Reporting
{
    public class XmlReport : IReport
    {
        #region IReport Members

        public void Generate (IEnumerable<MetricResult> results)
        {
            throw new NotImplementedException ();
        }

        public void Generate (MetricResult result)
        {
            throw new NotImplementedException ();
        }

        #endregion
    }
}
