using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DrivenMetrics.metrics;
using NUnit.Framework;

namespace DrivenMetrics.Tests
{
    [TestFixture]
    public class CyclomicComplexityTests
    {
        private AssemblySearcher _methodFinder;
        private const int _MaxCC = 20;
        
        [SetUp]
        public void Setup()
        {
            var assemblyLoader = new AssemblyLoader("DomainTestClasses.dll");
            var assembly = assemblyLoader.Load();
            _methodFinder = new AssemblySearcher(assembly);
        }

        /*[SetUp]
        public void Setup()
        {
           // drivenMetric = new DrivenMetric("DomainTestClasses.dll");
        }
        
        [Test]
        public void ShouldDetermineCyclomicComplexityForFooFirst()
        {

            var cc = drivenMetric.CalculateCyclomicComplexity("First");
            Assert.That(cc, Is.EqualTo(1));
        }

        [Test]
        public void ShouldDetermineCyclomicComplexityForFooSecond()
        {

            var cc = drivenMetric.CalculateCyclomicComplexity("Second");
            Assert.That(cc, Is.EqualTo(2));
        }

        [Test]
        public void ShouldDetermineCyclomicComplexityForFooThird()
        {

            var cc = drivenMetric.CalculateCyclomicComplexity("Third");
            Assert.That(cc, Is.EqualTo(3));
        }

        [Test]
        public void ShouldDetermineCyclomicComplexityForFooFourth()
        {

            var cc = drivenMetric.CalculateCyclomicComplexity("Fourth");
            Assert.That(cc, Is.EqualTo(5));
        }*/

        [Test]
        public void ShouldCalculateCCForAllMethodsInCollection()
        {
            var cyclomicCalculator = new CyclomicComplextityCalculator(_MaxCC);
            var types = _methodFinder.GetAllTypes();

            var 
                results = cyclomicCalculator.Calculate(types);

            var numberOfClasses = results.ClassResults.Count;
            var numberOfMethods = results.ClassResults[0].MethodResults.Count;


            Assert.That(numberOfClasses, Is.EqualTo(1));
            Assert.That(numberOfMethods, Is.EqualTo(5));
        }
    }
}
