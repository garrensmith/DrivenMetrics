using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Mono.Cecil;
using Driven.Metrics;
using Driven.Metrics.Metrics;

namespace DrivenMetrics.Tests.Metrics
{
    [TestFixture]
    public class CyclomaticComplexityReflectorMetricTest
    {
        private AssemblyDefinition assemblyDefinition;

        [SetUp]
        public void SetUp()
        {
            AssemblyLoader loader = new AssemblyLoader("DrivenMetrics.Tests.exe");
            assemblyDefinition = loader.Load();
        }

        [Test]
        public void SimpleTest()
        {
            IMetric cyclomaticComplexity = new CyclomaticComplexityReflectorMetric();
            MethodDefinition simpleMethod = Utils.GetMethod(assemblyDefinition, "DrivenMetrics.Tests.TestClasses.CodeComplexity", "SimpleMethod");
            MethodResult methodResult = cyclomaticComplexity.ProcessMethod(simpleMethod);
            Assert.AreEqual(1, methodResult.Result);
        }

        [Test]
        public void ComplexTest()
        {
            IMetric cyclomaticComplexity = new CyclomaticComplexityReflectorMetric();
            MethodDefinition simpleMethod = Utils.GetMethod(assemblyDefinition, "DrivenMetrics.Tests.TestClasses.CodeComplexity", "ComplexMethod");
            MethodResult methodResult = cyclomaticComplexity.ProcessMethod(simpleMethod);
            Assert.AreEqual(5, methodResult.Result);
        }

        [Test]
        public void IfTest()
        {
            IMetric cyclomaticComplexity = new CyclomaticComplexityReflectorMetric();
            MethodDefinition simpleMethod = Utils.GetMethod(assemblyDefinition, "DrivenMetrics.Tests.TestClasses.CodeComplexity", "IfMethod");
            MethodResult methodResult = cyclomaticComplexity.ProcessMethod(simpleMethod);
            Assert.AreEqual(3, methodResult.Result);
        }
    }
}
