using System.Xml.Linq;
using Driven.Metrics.Metrics;
using Driven.Metrics.Reporting;
using NUnit.Framework;

namespace Driven.Metrics.Tests.Reporting
{
    [TestFixture]
    public class XmlReportTest
    {
        private static void AssertAreEqual(string expectedXml, XmlReport actualReport)
        {
            var expectedElement = XElement.Parse (expectedXml);
            Assert.AreEqual (expectedElement.ToString (), actualReport.ToString ());
        }

        [Test]
        public void EmptyReport()
        {
            var report = new XmlReport (null, null);
            AssertAreEqual ("<metrics />", report);
        }

        [Test]
        public void GenerateNoResults()
        {
            // arrange
            var writer = new MemoryFileWriter ();
            const string filePath = "<memory>";
            var report = new XmlReport (writer, filePath);

            // act
            report.Generate (new MetricResult[] { });

            // assert
            Assert.AreEqual ("<metrics />", writer.Contents);
            Assert.AreEqual (filePath, writer.FilePath);
        }
    }
}
