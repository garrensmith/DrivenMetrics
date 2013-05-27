using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrivenMetrics.Tests.TestClasses
{
    public class CodeComplexity
    {
        public void SimpleMethod()
        {
        }

        public void ComplexMethod()
        {
            bool nextPage = false;
            int lineCount = 1;
            int linesPerPage = 1;
            int status = 1;
            int cancelled = 1;
            bool morePages = false;
            while (nextPage != true)
            {
                if ((lineCount <= linesPerPage) && (status != cancelled) && (morePages == true))
                {
                    //....
                }
            }
        }

        public void IfMethod()
        {
            int value;
            int param1 = 0;
            if (param1 == 0)
            {
                value = 4;
            }
            else
            {
                value = 0;
            }
            param1 = value;
        }
    }
}
