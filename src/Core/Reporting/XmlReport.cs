using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Driven.Metrics.Metrics;

namespace Driven.Metrics.Reporting
{
    public class XmlReport : IReport
    {
        private static readonly XmlWriterSettings XmlWriterSettings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "  ",
            OmitXmlDeclaration = true,
        };

        private readonly IFileWriter _fileWriter;
        private readonly string _filePath;
        private readonly XDocument _doc;

        public XmlReport(IFileWriter fileWriter, string filePath)
        {
            _fileWriter = fileWriter;
            _filePath = filePath;
            _doc = new XDocument(new XElement("metrics"));
        }

        public override string ToString ()
        {
            var sb = new StringBuilder ();
            using (var writer = XmlWriter.Create (sb, XmlWriterSettings))
            {
                Debug.Assert (writer != null);
                _doc.WriteTo (writer);
            }
            return sb.ToString ();
        }

        internal static XElement ConvertResult(MetricResult metricResult)
        {
            #region <metric name="Cyclomatic Complexity">
            var metric = new XElement ("metric", new XAttribute("name", metricResult.Name));
            foreach (var classResult in metricResult.ClassResults)
            {
                var @class = ConvertResult (classResult);
                metric.Add (@class);
            }
            #endregion
            return metric;
        }

        internal static XElement ConvertResult(ClassResult classResult)
        {
            #region <class name="XmlReport">
            var @class = new XElement ("class", new XAttribute("name", classResult.Name));
            foreach (var methodResult in classResult.MethodResults)
            {
                var method = ConvertResult (methodResult);
                @class.Add (method);
            }
            #endregion
            return @class;
        }

        internal static XElement ConvertResult(MethodResult methodResult)
        {
            #region <method name="ConvertResult" pass="true" result="1" />
            var method = new XElement ("method", 
                new XAttribute ("name", methodResult.Name),
                new XAttribute ("pass", methodResult.Pass),
                new XAttribute ("result", methodResult.Result)
            );
            return method;
            #endregion
        }

        #region IReport Members

        public void Generate (IEnumerable<MetricResult> results)
        {
            var root = _doc.Root;
            Debug.Assert (root != null);
            foreach (var metricResult in results)
            {
                var metricElement = ConvertResult (metricResult);
                root.Add (metricElement);
            }
            var contents = ToString ();
            _fileWriter.Write (_filePath, contents);
        }

        public void Generate (MetricResult result)
        {
            Generate (new [] { result});
        }

        #endregion
    }
}
