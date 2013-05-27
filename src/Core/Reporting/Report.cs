﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Xsl;

namespace Driven.Metrics.Reporting
{
    public class Report : IReport
    {
        public string XsltFilename { get; set; }

        public string ReportFilename { get; set; }

        public Report(string reportFilename, string xsltFilename)
            : this()
        {
            XsltFilename = xsltFilename;
            ReportFilename = reportFilename;
        }

        public Report()
        {
        }

        public void Generate(params Metrics.MetricResult[] metricResults)
        {
            ReportResult result = new ReportResult();
            result.MetricResults = metricResults;
            XDocument doc = Serialize(result);
            Transform(doc);
        }

        private XDocument Serialize<T>(T source)
        {
            XDocument target = new XDocument();
            XmlSerializer s = new XmlSerializer(typeof(T));
            System.Xml.XmlWriter writer = target.CreateWriter();
            s.Serialize(writer, source);
            writer.Close();
            return target;
        }

        private void Transform(XDocument doc)
        {
            XslCompiledTransform xmlTransform = new XslCompiledTransform();
            XmlWriter writer = XmlWriter.Create(ReportFilename);
            xmlTransform.Load(XsltFilename);
            xmlTransform.Transform(doc.CreateReader(), writer);
        }

    }
}
