using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDesk.Options;
using NUnit.Framework;

namespace DrivenMetrics.Tests.Ndesk.Options
{
    [TestFixture]
    public class Ndesk
    {
        [Test]
        public void CanItLoadMultipleAssembliesWithOneCommand()
        {
            bool show_help = false;
            List<string> names = new List<string>();
            int repeat = 1;
            int verbosity = 0;
            string assemblies;

            var p = new OptionSet()
                        {
                            {
                                "n|name=", "the {NAME} of someone to greet.",
                                v => names.Add(v)
                                },
                            {
                                "r|repeat=",
                                "the number of {TIMES} to repeat the greeting.\n" +
                                "this must be an integer.",
                                (int v) => repeat = v
                                },
                            {
                                "v", "increase debug message verbosity",
                                v => { if (v != null) ++verbosity; }
                                },
                            {
                                "h|help", "show this message and exit",
                                v => show_help = v != null
                                }, 
                            {
                               "a|a=", 
                               "Assemblies to analyze",
                               v => { assemblies = v;}
                               },
                        };

            string[] args = new string[] {"-a=test3.dll","test.dll","test2.dll","-n","A"};

            p.Parse(args);

            verbosity++;

        }

    }
}
