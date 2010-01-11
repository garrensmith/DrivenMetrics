using DrivenMetric.UI.Console;
using NUnit.Framework;
using System;
using DrivenMetrics.Reporting;

namespace DrivenMetrics.Tests.Console
{
    [TestFixture]
    public class ConsoleArgumentsTest
    {
		
		// drivenmetric.exe -a test1.dll test2.dll -loc -cc -r output.html

        [Test]
        public void ShouldSetHelpToTrue()
        {
            var args = new[] {"/?"};

            var argumentparser = new ConsoleArgumentParser(args);

            var arguments = argumentparser.Parse();

            Assert.That(arguments.Help,Is.True);
        }

        [Test]
        public void ShouldSetHelpToTrueIfNoArgumentsGiven()
        {
            var args = new string[0];

            var argumentparser = new ConsoleArgumentParser(args);

            var arguments = argumentparser.Parse();

            Assert.That(arguments.Help, Is.True);
        }
		
		[Test]
		public void ShouldSetHelpToFalseIfArgmentsGiven()
		{
			var args = new[] {"-a","fake.dll","-r","output.html"};
			
			var argumentparser = new ConsoleArgumentParser(args);

            var arguments = argumentparser.Parse();

            Assert.That(arguments.Help, Is.False);
		}
		
		[Test]
        public void ShouldCreateListOfAssembliesWithOneAssembly()
        {
            var assemblyName1 = "Test.dll";
            
            
            var args = new[] {"-a",assemblyName1,"-r","output.html"};

            var argumentparser = new ConsoleArgumentParser(args);
            var arguments = argumentparser.Parse();

            Assert.That(arguments.AssemblyNames[0],Is.EqualTo(assemblyName1));
            

        }

        [Test]
        public void ShouldCreateListOfAssembliesWithCommandsBefore()
        {
            var assemblyName1 = "Test.dll";
            var assemblyName2 = "Test2.dll";
            
            var args = new[] { "-r","output.html","-a",assemblyName1,assemblyName2};

            var argumentparser = new ConsoleArgumentParser(args);
            var arguments = argumentparser.Parse();

            Assert.That(arguments.AssemblyNames[0],Is.EqualTo(assemblyName1));
            Assert.That(arguments.AssemblyNames[1], Is.EqualTo(assemblyName2));

        }
		
		[Test]
        public void ShouldCreateListOfAssembliesWithCommandsFollowing()
        {
            var assemblyName1 = "Test.dll";
            var assemblyName2 = "Test2.dll";
            
            var args = new[] {"-a",assemblyName1,assemblyName2, "-r","output.html"};

            var argumentparser = new ConsoleArgumentParser(args);
            var arguments = argumentparser.Parse();

            Assert.That(arguments.AssemblyNames[0],Is.EqualTo(assemblyName1));
            Assert.That(arguments.AssemblyNames[1], Is.EqualTo(assemblyName2));
        }
		
		[Test]
		public void ShouldCreateOutputReportFileName()
		{
			var reportName = "output.html";
            var args = new[] {"-a","fake.dll","-r",reportName};

            var argumentparser = new ConsoleArgumentParser(args);
            var arguments = argumentparser.Parse();

            Assert.That(arguments.ReportName,Is.EqualTo(reportName));
        }
		
		[Test]
		public void ShouldThrowExceptionIfNoOutputReportGivenWithCommand()
		{
            var args = new[] {"-r","-loc"};

            var argumentparser = new ConsoleArgumentParser(args);
            Assert.Throws<Exception>(() => argumentparser.Parse(), "No report file name given");            		
		}
		
		[Test]
		public void ShouldThrowExceptionIfNoOutputReport()
		{
            var args = new[] {"-loc"};

            var argumentparser = new ConsoleArgumentParser(args);
            Assert.Throws<Exception>(() => argumentparser.Parse(), "No report file name given");            		
		}
		
		[Test]
		public void ShouldLoadCyclomicComplexityMetric()
		{
			var args = new[] {"-cc","10","-r","output.html","-a","fake.dll"};

            var argumentparser = new ConsoleArgumentParser(args);
            var arguments = argumentparser.Parse();

            Assert.That(arguments.Metrics[0].ToString(),Is.EqualTo("DrivenMetrics.metrics.ILCyclomicComplextityCalculator"));	
			Assert.That(arguments.Metrics[0].MaxPassValue,Is.EqualTo(10));
		}
		
		[Test]
		public void ShouldLoadCyclomicComplexityMetricWithDefaultMaxValue1()
		{
			
			var args = new[] {"-a","fake.dll","-cc","-v","-r","output.html"};

            var argumentparser = new ConsoleArgumentParser(args);
            var arguments = argumentparser.Parse();

            Assert.That(arguments.Metrics[0].ToString(),Is.EqualTo("DrivenMetrics.metrics.ILCyclomicComplextityCalculator"));
			Assert.That(arguments.Metrics[0].MaxPassValue,Is.EqualTo(15));			            
		}
		
		[Test]
		public void ShouldLoadCyclomicComplexityMetricWithDefaultMaxValue2()
		{
			
			var args = new[] {"-a","fake.dll","-cc","-r","output.html"};

            var argumentparser = new ConsoleArgumentParser(args);
            var arguments = argumentparser.Parse();

            Assert.That(arguments.Metrics[0].ToString(),Is.EqualTo("DrivenMetrics.metrics.ILCyclomicComplextityCalculator"));
			Assert.That(arguments.Metrics[0].MaxPassValue,Is.EqualTo(15));			            
		}
		
		[Test]
		public void ShouldLoadLinesOfCodeCalculatorWithDefaultValue()
		{
			var args = new[] {"-loc","-r","output.html","-a","fake.dll"};

            var argumentparser = new ConsoleArgumentParser(args);
            var arguments = argumentparser.Parse();

            Assert.That(arguments.Metrics[0].ToString(),Is.EqualTo("DrivenMetrics.metrics.NumberOfLinesCalculator"));
			Assert.That(arguments.Metrics[0].MaxPassValue,Is.EqualTo(15));
			
		}
		
		[Test]
		public void ShouldLoadCorrectReportForAllReportCommand()
		{
			var args = new[] {"-rAll","output.html","-a","fake.dll"};

            var argumentparser = new ConsoleArgumentParser(args);
            var arguments = argumentparser.Parse();
			
			Assert.That(arguments.ReportName,Is.EqualTo("output.html"));
			Assert.That(arguments.ReportType, Is.EqualTo(ReportType.All));
			            
		}
		
		[Test]
		public void ShouldLoadCorrectReportForFailingReportCommand()
		{
			var args = new[] {"-rFail","output.html","-a","fake.dll"};

            var argumentparser = new ConsoleArgumentParser(args);
            var arguments = argumentparser.Parse();
			
			Assert.That(arguments.ReportName,Is.EqualTo("output.html"));
			Assert.That(arguments.ReportType, Is.EqualTo(ReportType.Failing));
			            
		}
		
		[Test]
		[Ignore]
		public void ShouldThrowUknownCommand()
		{
			var args = new[] {"-unk"};

            var argumentparser = new ConsoleArgumentParser(args);
            Assert.Throws<Exception>(() => argumentparser.Parse(),"Unknown Command -unk");
			
		}
    }
}
