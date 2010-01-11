using System.IO;
using NUnit.Framework;
using Mono.Cecil;
using Mono.Cecil.Cil;

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
		
		[Test]
        [Ignore]
		public void ShouldLoadSymbols()
		{
			var assembly = AssemblyFactory.GetAssembly("DomainTestClasses.dll");
			
			assembly.MainModule.LoadSymbols();
			
			foreach(TypeDefinition t in assembly.MainModule.Types)
			{
				foreach(MethodDefinition m in t.Methods)
				{
					foreach(Instruction ins in m.Body.Instructions)
					{
						if (ins.SequencePoint != null)
							System.Console.WriteLine(ins.SequencePoint.ToString());
					}
				}
				
				System.Console.WriteLine(t.ToString());
			}
			
		}

       /* public void ShouldThrowExceptionOnMissingPdb()
        {
            var assemblyLoader = new AssemblyLoader("Mono.Cecil.dll");
            var assembly = assemblyLoader.Load();
            Assert.That(assembly, Is.Not.Null);
        }*/

    }
}
