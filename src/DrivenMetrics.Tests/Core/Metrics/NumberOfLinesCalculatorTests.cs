using Mono.Cecil;
using NUnit.Framework;
using Driven.Metrics.metrics;
using Driven.Metrics;

namespace Driven.Metrics.Tests.Core.Metrics
{
    [TestFixture]
    public class NumberOfLinesCalculatorTests
    {
        private AssemblySearcher _assemblySearcher;
        private const int _MaxLines = 20;

        [SetUp]
        public void Setup()
        {
            var assemblyLoader = new AssemblyLoader("DomainTestClasses.dll");
            var assembly = assemblyLoader.Load();
            _assemblySearcher = new AssemblySearcher(assembly);
        }

        [Test]
        public void ShouldCalculateNumberOfLinesForFooFirst()
        {
            var method = _assemblySearcher.FindMethod("First");
            var lineCalculator = new NumberOfLinesCalculator(_MaxLines);
            var loc = lineCalculator.Calculate(method);

            Assert.That(loc.Result, Is.InRange(1, 4));
        }

        [Test]
        public void ShouldCalculateNumberOfLinesForFooSecond()
        {
            var method = _assemblySearcher.FindMethod("Second");
            var lineCalculator = new NumberOfLinesCalculator(_MaxLines);
            var loc = lineCalculator.Calculate(method);

            Assert.That(loc.Result, Is.InRange(2,6));
        }

        [Test]
        public void ShouldCalculateNumberOfLinesForFooThird()
        {
            var method = _assemblySearcher.FindMethod("Third");
            var lineCalculator = new NumberOfLinesCalculator(_MaxLines);
            var loc = lineCalculator.Calculate(method);

            Assert.That(loc.Result, Is.InRange(4, 6));
        }

        [Test]
        public void ShouldCalculateNumberOfLinesForFooFourth()
        {
            var method = _assemblySearcher.FindMethod("Fourth");
            var lineCalculator = new NumberOfLinesCalculator(_MaxLines);
            var loc = lineCalculator.Calculate(method);

            Assert.That(loc.Result, Is.InRange(11, 19));
        }

        [Test]
        public void ShouldCalculateNoLForAllMethodsInCollection()
        {
            var lineCalculator = new NumberOfLinesCalculator(_MaxLines);
            var types = _assemblySearcher.GetAllTypes();

            var results = lineCalculator.Calculate(types);
            
            var numberOfClasses = results.ClassResults.Count;
            var numberOfMethods = results.ClassResults[0].MethodResults.Count;


            Assert.That(numberOfClasses,Is.EqualTo(2));
            Assert.That(numberOfMethods,Is.EqualTo(6));
        }
    }
}