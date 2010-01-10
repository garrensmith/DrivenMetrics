using Mono.Cecil;
using NUnit.Framework;
using DrivenMetrics.metrics;

namespace DrivenMetrics.Tests.Core
{
    [TestFixture]
    public class NumberOfLinesCalculatorTests
    {
        private AssemblySearcher _methodFinder;
        private const int _MaxLines = 20;

        [SetUp]
        public void Setup()
        {
            var assemblyLoader = new AssemblyLoader("DomainTestClasses.dll");
            var assembly = assemblyLoader.Load();
            _methodFinder = new AssemblySearcher(assembly);
        }

        [Test]
        public void ShouldCalculateNumberOfLinesForFooFirst()
        {
            var method = _methodFinder.Find("First");
            var lineCalculator = new NumberOfLinesCalculator(_MaxLines);
            var loc = lineCalculator.Calculate(method);

            Assert.That(loc.Result, Is.InRange(2, 4));
        }

        [Test]
        public void ShouldCalculateNumberOfLinesForFooSecond()
        {
            var method = _methodFinder.Find("Second");
            var lineCalculator = new NumberOfLinesCalculator(_MaxLines);
            var loc = lineCalculator.Calculate(method);

            Assert.That(loc.Result, Is.InRange(4,6));
        }

        [Test]
        public void ShouldCalculateNumberOfLinesForFooThird()
        {
            var method = _methodFinder.Find("Third");
            var lineCalculator = new NumberOfLinesCalculator(_MaxLines);
            var loc = lineCalculator.Calculate(method);

            Assert.That(loc.Result, Is.InRange(4, 6));
        }

        [Test]
        public void ShouldCalculateNumberOfLinesForFooFourth()
        {
            var method = _methodFinder.Find("Fourth");
            var lineCalculator = new NumberOfLinesCalculator(_MaxLines);
            var loc = lineCalculator.Calculate(method);

            Assert.That(loc.Result, Is.InRange(11, 15));
        }

        [Test]
        public void ShouldCalculateNoLForAllMethodsInCollection()
        {
            var lineCalculator = new NumberOfLinesCalculator(_MaxLines);
            var types = _methodFinder.GetAllTypes();

            var results = lineCalculator.Calculate(types);
            
            var numberOfClasses = results.ClassResults.Count;
            var numberOfMethods = results.ClassResults[0].MethodResults.Count;


            Assert.That(numberOfClasses,Is.EqualTo(1));
            Assert.That(numberOfMethods,Is.EqualTo(5));
        }
    }
}