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
            NewLineChars = "\n",
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

        #region IReport Members

        public void Generate (IEnumerable<MetricResult> results)
        {
            // TODO: convert results to nodes and add them to _doc
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
