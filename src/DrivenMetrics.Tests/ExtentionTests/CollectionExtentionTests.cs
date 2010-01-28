using System.Collections.Generic;
using System.Linq;
using Mono.Cecil.Extensions;
using NUnit.Framework;
using Mono.Cecil;
using Driven.Metrics;

namespace Driven.Metrics.Tests.ExtentionTests
{
    [TestFixture]
    public class CollectionExtentionTests
    {
        private AssemblySearcher _methodFinder;
        
        [SetUp]
        public void Setup()
        {
            var assemblyLoader = new AssemblyLoader("DomainTestClasses.dll");
            var assembly = assemblyLoader.Load();
            _methodFinder = new AssemblySearcher(assembly);
        }
        
        [Test]
        public void ShouldReturnAllInstructionsWithSequencePoints()
        {
            var method = _methodFinder.FindMethod("First");

            var instructions = method.Body.Instructions.WithSequencePoint();

            foreach (var instruction in instructions)
                Assert.That(instruction.SequencePoint, Is.Not.Null);
        }

        [Test]
        public void ShouldReturnAllMethodsWithBodies()
        {
            var types = _methodFinder.GetAllTypes();

            foreach (var type in types)
            {
                foreach (MethodDefinition methodDefinition in type.Methods.WithBodys())
                    Assert.That(methodDefinition.Body, Is.Not.Null);
            }
        }

        [Test]
        public void ShouldNotReturnGettersOrSetters()
        {
            var types = _methodFinder.GetAllTypes();

            foreach (var type in types)
            {
                foreach (MethodDefinition methodDefinition in type.Methods.WithBodys())
                {
                    Assert.That(methodDefinition.IsSetter, Is.False);
                    Assert.That(methodDefinition.IsGetter, Is.False);
                }
            }
        }

        [Test]
        public void ShouldNotReturnConstructor()
        {
            var types = _methodFinder.GetAllTypes();

            foreach (var type in types)
            {
                foreach (MethodDefinition methodDefinition in type.Methods.WithBodys())
                {
                    Assert.That(methodDefinition.IsConstructor, Is.False);
                }
            }
        }

        [Test]
        public void ShouldNotReturnAnyCompilerMadeMethods()
        {
            var types = _methodFinder.GetAllTypes();

            foreach (var type in types)
            {
                foreach (MethodDefinition methodDefinition in type.Methods.WithBodys())
                {
                    Assert.That(methodDefinition.Name.Contains("__"), Is.False);
                }
            }
        }

    }
}
