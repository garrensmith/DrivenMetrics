using NUnit.Framework;
using Rhino.Mocks;
using Driven.Metrics.metrics;
using Driven.Metrics.Reporting;
using Mono.Cecil;
using System.Collections.Generic;
using Driven.Metrics.Tests.TestBuilders;
using Driven.Metrics.Interfaces;
using Driven.Metrics;

namespace Driven.Metrics.Tests.Core
{
    [TestFixture]
    public class DrivenMetricTests
    {
        [Test]
		[Ignore]
        public void ShouldCreateInstanceWithOneAssembly()
        {
            var drivenMetric = new DrivenMetrics.Factory().Create(@"C:\Projects\Keyblade\KeyBlade\KeyBlade.DiConfiguration\bin\KeyBlade.Core.dll",
			                                                     @"C:\Projects\DrivenMetrics\output.html");
            drivenMetric.RunAllMetricsAndGenerateReport();
            
            Assert.That(drivenMetric,Is.Not.Null);
        }

        [Test]
        public void ShouldCreateInstanceWithMultipleAssemblies()
        {
            var drivenMetric = new DrivenMetrics.Factory().Create(new[] { "DomainTestClasses.dll", "DomainTestClasses.dll" },
																@"C:\Projects\DrivenMetrics\output.html");

            Assert.That(drivenMetric, Is.Not.Null);
        }

        [Test]
        public void ShouldCreateInstanceWithManyAssembies()
        {
            var drivenMetric = new DrivenMetrics.Factory().Create(new[] { "DomainTestClasses.dll", "DomainTestClasses.dll" }
																,@"C:\Projects\DrivenMetrics\output.html");
            Assert.That(drivenMetric, Is.Not.Null);
        }
        
 
        [Test]
        public void ShouldRunAllMetricsAndGenerateReport()
        {
            var typeDefnintions = new TypeDefinitionListBuilder().CreateTypeDefinitionList();
            var metricResults = new MetricResultBuilder().CreateMetricResult();
            var assemblySearcher = MockRepository.GenerateMock<IAssemblySearcher>();
            var metric1 = MockRepository.GenerateMock<IMetricCalculator>();
            var metric2 = MockRepository.GenerateMock<IMetricCalculator>();
            var report = MockRepository.GenerateMock<IReport>();

            assemblySearcher.Expect(x => x.GetAllTypes())
                .Repeat.AtLeastOnce()
                .Return(typeDefnintions);

            metric1.Expect(x => x.Calculate(new List<TypeDefinition>()))
                .IgnoreArguments()
                .Return(metricResults);

            metric2.Expect(x => x.Calculate(new List<TypeDefinition>()))
                .IgnoreArguments()
                .Return(metricResults);

            //report.Expect(x => x.Generate(metricResults, "")).IgnoreArguments();
            
            var dMetric = new DrivenMetrics(assemblySearcher, report, new[] {metric1, metric2});
            dMetric.RunAllMetricsAndGenerateReport();

            assemblySearcher.AssertWasCalled(x => x.GetAllTypes(), options => options
                                                                              .IgnoreArguments()
                                                                              .Repeat
                                                                              .AtLeastOnce());

            metric1.AssertWasCalled(x => x.Calculate(new List<TypeDefinition>()), options => options.IgnoreArguments());
            metric2.AssertWasCalled(x => x.Calculate(new List<TypeDefinition>()), options => options.IgnoreArguments());
            //report.AssertWasCalled(x => x.Generate(metricResults, ""), options => options.IgnoreArguments());
        }
        
    }
}