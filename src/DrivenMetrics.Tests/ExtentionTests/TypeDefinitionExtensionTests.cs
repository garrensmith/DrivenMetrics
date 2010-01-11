using Mono.Cecil.Extensions;
using NUnit.Framework;

namespace DrivenMetrics.Tests.ExtentionTests
{
    [TestFixture]
    public class TypeDefinitionExtensionTests
    {

        [Test]
        public void ShouldReturnTrueForValidType()
        {
            var assemblyLoader = new AssemblyLoader("DomainTestClasses.dll");
            var assembly = assemblyLoader.Load();
            var result = assembly.MainModule.Types["DomainTestClasses.Foo"].IsValidForMetrics();

            Assert.That(result,Is.True);
        }

        [Test]
        public void ShouldReturnFalseForInMainModule()
        {
            var assemblyLoader = new AssemblyLoader("DomainTestClasses.dll");
            var assembly = assemblyLoader.Load();
            var result = assembly.MainModule.Types[0].IsValidForMetrics();

            Assert.That(result, Is.False);
        }

        [Test]
        public void ShouldReturnFalseForCompilerMadeMethod()
        {
            var assemblyLoader = new AssemblyLoader("DomainTestClasses.dll");
            var assembly = assemblyLoader.Load();
            var result = assembly.MainModule.Types[0].IsValidForMetrics();

            Assert.That(result, Is.False);
        }

    }
}
