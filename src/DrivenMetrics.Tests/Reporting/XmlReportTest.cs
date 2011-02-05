using System.Xml.Linq;
using Driven.Metrics.Metrics;
using Driven.Metrics.Reporting;
using Driven.Metrics.Tests.TestBuilders;
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

        [Test]
        public void ConvertSimpleMethodResult()
        {
            var methodResult = new MethodResult ("ConvertResult", 12, true);
            var actual = XmlReport.ConvertResult (methodResult);
            Assert.AreEqual (@"<method name=""ConvertResult"" pass=""true"" result=""12"" />", actual.ToString ());
        }

        [Test]
        public void ConvertEmptyClassResult()
        {
            var classResult = new ClassResult ("XmlReport", new MethodResult[] {});
            var actual = XmlReport.ConvertResult (classResult);
            Assert.AreEqual (@"<class name=""XmlReport"" />", actual.ToString ());
        }

        [Test]
        public void ConvertEmptyMetricResult ()
        {
            var metricResult = new MetricResult ("Cyclomatic Complexity", new ClassResult[] { });
            var actual = XmlReport.ConvertResult (metricResult);
            Assert.AreEqual (@"<metric name=""Cyclomatic Complexity"" />", actual.ToString ());
        }

        [Test]
        public void GenerateSampleMetricResult()
        {
            var instance = new XmlReport (new MemoryFileWriter (), null);
            var sampleMetricResult = new MetricResultBuilder ().CreateMetricResult ();
            instance.Generate (sampleMetricResult);
            AssertAreEqual (@"
<metrics>
    <metric name=""Test Metric"">
        <class name=""Test Class 1"">
            <method name=""Test Method1"" pass=""false"" result=""12"" />
            <method name=""Test Method2"" pass=""true"" result=""5"" />
        </class>
        <class name=""Test Class 1"">
            <method name=""Test Method1"" pass=""false"" result=""12"" />
            <method name=""Test Method2"" pass=""true"" result=""5"" />
        </class>
    </metric>
</metrics>", instance);
        }
    }
}
