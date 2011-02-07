using Driven.Metrics.metrics;
using NUnit.Framework;
using Driven.Metrics;

namespace Driven.Metrics.Tests.Core.Metrics
{
    [TestFixture]
    public class ILCyclomicComplexityTests
    {
        private AssemblySearcher _assemblySearcher;
        private const int _MaxCC = 20;
        

        [SetUp]
        public void Setup()
        {
            var assemblyLoader = new AssemblyLoader("DomainTestClasses.dll");
            var assembly = assemblyLoader.Load();
            _assemblySearcher = new AssemblySearcher(assembly);
        }
        
        [Test]
        public void ShouldDetermineCyclomicComplexityForFooFirst()
        {
            var method = _assemblySearcher.FindMethod("First");
            
            var cyclomicComplextityCalculator = new ILCyclomicComplextityCalculator(_MaxCC);
            var cc = cyclomicComplextityCalculator.Calculate(method);
            
            Assert.That(cc.Result, Is.EqualTo(1));
        }

        [Test]
        public void ShouldDetermineCyclomicComplexityForFooSecond()
        {

            var method = _assemblySearcher.FindMethod("Second");

            var cyclomicComplextityCalculator = new ILCyclomicComplextityCalculator(_MaxCC);
            var cc = cyclomicComplextityCalculator.Calculate(method);

            Assert.That(cc.Result, Is.EqualTo(3));
        }

        [Test]
        public void ShouldDetermineCyclomicComplexityForFooThird()
        {
            var method = _assemblySearcher.FindMethod("Third");

            var cyclomicComplextityCalculator = new ILCyclomicComplextityCalculator(_MaxCC);
            var cc = cyclomicComplextityCalculator.Calculate(method);

            Assert.That(cc.Result, Is.InRange(3, 7)); // actual 3
        }

        [Test]
        public void ShouldDetermineCyclomicComplexityForFooFourth()
        {
            var method = _assemblySearcher.FindMethod("Fourth");

            var cyclomicComplextityCalculator = new ILCyclomicComplextityCalculator(_MaxCC);
            var cc = cyclomicComplextityCalculator.Calculate(method);
            
            Assert.That(cc.Result, Is.InRange(5,10)); //actual 5
        }

        [Test]
        public void ShouldCalculateCCForAllMethodsInCollection()
        {
            var cyclomicCalculator = new ILCyclomicComplextityCalculator(_MaxCC);
            var types = _assemblySearcher.GetAllTypes();

            var results = cyclomicCalculator.Calculate(types);

            var numberOfClasses = results.ClassResults.Count;
            var numberOfMethods = results.ClassResults[0].MethodResults.Count;
            
            Assert.That(numberOfClasses, Is.EqualTo(2));
            Assert.That(numberOfMethods, Is.EqualTo(6));
        }
    }
}