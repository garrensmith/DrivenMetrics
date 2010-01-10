using System.Linq;
using NUnit.Framework;
using Mono.Cecil;
namespace DrivenMetrics.Tests
{
    [TestFixture]
    public class AssemblySearcherTests
    {
        private AssemblyDefinition _assembly;

        [SetUp]
        public void Setup()
        {
            var assemblyLoader = new AssemblyLoader("DomainTestClasses.dll");
            _assembly = assemblyLoader.Load();
        }
        
        [Test]
        public void ShouldLoadValidMethod()
        {
            var methodFinder = new AssemblySearcher(_assembly);
            var method = methodFinder.Find("First");

            Assert.That(method.Name,Is.EqualTo("First"));
        }

        [Test]
        public void ShouldReturnNullForInvalidMethod()
        {
            var methodFinder = new AssemblySearcher(_assembly);
            var method = methodFinder.Find("NonExisting");

            Assert.That(method, Is.Null);
        }

        [Test]
        public void ShouldGetAllTypes()
        {
            var methodFinder = new AssemblySearcher(_assembly);
            var types = methodFinder.GetAllTypes().ToList();
            
            Assert.That(types.Count,Is.EqualTo(2));
        }

    }
}
