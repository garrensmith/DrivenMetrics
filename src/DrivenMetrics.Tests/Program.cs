using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DrivenMetrics.Tests
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            NUnit.Gui.AppEntry.Main(new string[]
                {
                    Assembly.GetExecutingAssembly().Location, 
                    "/run"
                });

        }
    }
}
