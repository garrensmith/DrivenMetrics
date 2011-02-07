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
            var metric = new XElement ("metric", new XAttribute("name", metricResult.Name));
            foreach (var classResult in metricResult.ClassResults)
            {
                var convertedResults = ConvertResult (classResult);
                metric.Add (convertedResults);
            }
            
            return metric;
        }

        internal static XElement ConvertResult(ClassResult classResult)
        {
            var xElement = new XElement ("class", new XAttribute("name", classResult.Name));
            foreach (var methodResult in classResult.MethodResults)
            {
                var method = ConvertResult (methodResult);
                xElement.Add (method);
            }
            
            return xElement;
        }

        internal static XElement ConvertResult(MethodResult methodResult)
        {
            var method = new XElement ("method", 
                new XAttribute ("name", methodResult.Name),
                new XAttribute ("pass", methodResult.Pass),
                new XAttribute ("result", methodResult.Result)
            );
            return method;
            
        }
  
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
    }
}
