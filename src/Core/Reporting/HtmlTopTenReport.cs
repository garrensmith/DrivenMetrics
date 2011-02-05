using System.Collections.Generic;
using System.Linq;
using System.Web;
using Driven.Metrics.Metrics;

namespace Driven.Metrics.Reporting
{
    public class HtmlTopTenReport : HtmlReport
    {
        public HtmlTopTenReport(IFileWriter fileWriter, string filePath)
            : base(fileWriter, filePath)
        {
        }

        protected override void inputResults(MetricResult result)
        {
            var flatResults = flatten(result);

            var topTen = (from f in flatResults
                          orderby f.Result descending
                          select f).Take(10);

            foreach (var tt in topTen)
            {
                Contents += "<tr>";
                Contents += "<td>" + _emptyColumn + "</td>";

                Contents += "<td>" + HttpUtility.HtmlEncode(tt.Name) + "</td>";

                if (tt.Pass)
                    Contents += @"<td id =""pass"">" + tt.Result + "</td>";
                else
                    Contents += @"<td id =""fail"">" + tt.Result + "</td>";

                Contents += "</tr>";
            }
        }

        private List<FlatResult> flatten(MetricResult result)
        {
            var flatResults = new List<FlatResult>();

            foreach (var classResult in result.ClassResults)
            {
                flatResults.AddRange(classResult.MethodResults.Select(methodResult => new FlatResult(string.Format("{0}.{1}", classResult.Name, methodResult.Name), methodResult.Result, methodResult.Pass)));
            }

            return flatResults;
        }
    }

    public class FlatResult
    {
        public int Result { get; private set; }
        public string Name { get; private set; }
        public bool Pass { get; private set; }

        public FlatResult(string name, int result, bool pass)
        {
            Name = name;
            Result = result;
            Pass = pass;
        }
    }
}