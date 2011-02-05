using System.Xml.Linq;
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
    }
}
