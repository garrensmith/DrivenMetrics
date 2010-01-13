
using System;
using System.Collections.Generic;
using DrivenMetrics.metrics;
using DrivenMetrics.Reporting;

namespace DrivenMetric.UI.Console
{
    [Obsolete]
    public class ConsoleArgumentParser
    {
        private readonly List<string> _args;
				
        public ConsoleArgumentParser(IEnumerable<string> args)
        {
            _args =  new List<string>(args);
								
        }
	
        public ConsoleArguments Parse()
        {
            var arguments = new ConsoleArguments();
	
            if (parseForHelpCommand (ref arguments) == true)
                return arguments;		
				
            parseForAssemblies(ref arguments);
				
            parseForMetrics (ref arguments);
	
            parseForReport(ref arguments);
	            
            return arguments;
        }
	
        private void parseForAssemblies (ref ConsoleArguments arguments)
        {
            int argIndex = getIndexForCommand("-a",true);
            int n = 1;
				
				
				
            while(!char.IsPunctuation(_args[argIndex + n][0]))
            {			
                arguments.AssemblyNames.Add(_args[argIndex + n]);
                n++;
					
                if (argIndex + n >= _args.Count)
                    break;
            }
        }
	
        private int getIndexForCommand (string command, bool throwExceptionIfMissing)
        {
            int argIndex = _args.FindIndex(item => item.Contains(command));
				
            if (throwExceptionIfMissing && argIndex < 0)
                throw new Exception("Missing command " + command);
				
            return argIndex;
        }
	        
		
        //TODO refactor into new metric parser
        private void parseForMetrics (ref ConsoleArguments arguments)
        {
            int argIndex = getIndexForCommand("-cc",false);
				
            if (argIndex > -1)
            {
                int maxCapacity;
                if (_args.Count <= argIndex + 1)
                    maxCapacity = 15;
					
                else if (int.TryParse(_args[argIndex +1], out maxCapacity) == false)
                    maxCapacity = 15;
					
                arguments.Metrics.Add(new ILCyclomicComplextityCalculator(maxCapacity));	
            }
				
            argIndex = getIndexForCommand("-loc",false);
				
            if (argIndex > -1)
            {
                int maxCapacity;
                if (_args.Count <= argIndex + 1)
                    maxCapacity = 15;
					
                else if (int.TryParse(_args[argIndex +1], out maxCapacity) == false)
                    maxCapacity = 15;
					
                arguments.Metrics.Add(new NumberOfLinesCalculator(maxCapacity));	
            }
        }
	        
        private void parseForReport (ref ConsoleArguments arguments)
        {
            int argIndex = getIndexForCommand("-r", true);
				
            if (!char.IsLetterOrDigit(_args[argIndex +1][0]))
                throw new Exception("No report file name given");
					  
            arguments.ReportName = _args[argIndex +1];
			
            if (_args[argIndex] == "-rFail")
                arguments.ReportType = ReportType.Failing;
            else
                arguments.ReportType = ReportType.All;
				
        }
	        
        private bool parseForHelpCommand (ref ConsoleArguments arguments)
        {
            if (_args.Count == 0 || _args.Contains("/?"))
                arguments.Help = true;                            
				
            return arguments.Help;
        }		
    }
}