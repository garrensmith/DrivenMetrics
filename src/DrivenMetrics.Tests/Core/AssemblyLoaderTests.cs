using System.IO;
using NUnit.Framework;

namespace DrivenMetrics.Tests
{
    [TestFixture]
    public class AssemblyLoaderTests
    {
        [Test]
        public void ShouldLoadAssembly()
        {
            var assemblyLoader = new AssemblyLoader("DomainTestClasses.dll");
            var assembly = assemblyLoader.Load();
            Assert.That(assembly,Is.Not.Null);
        }

        [Test]
        public void ShouldThrowExceptionOnMissingFile()
        {
            var assemblyLoader = new AssemblyLoader("fake.dll");
            Assert.Throws <FileNotFoundException>(() => assemblyLoader.Load());
        }

       /* public void ShouldThrowExceptionOnMissingPdb()
        {
            var assemblyLoader = new AssemblyLoader("Mono.Cecil.dll");
            var assembly = assemblyLoader.Load();
            Assert.That(assembly, Is.Not.Null);
        }*/

    }
}
