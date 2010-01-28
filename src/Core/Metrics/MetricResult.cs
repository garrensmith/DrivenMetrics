using System.Collections.Generic;

namespace Driven.Metrics.Metrics
{
    public class MethodResult
    {
        public string Name {get; private set;}
        public int Result {get; private set;}
        public bool Pass { get; private set; }
		
        public MethodResult(string name, int result, bool pass)
        {
            Name = name;
            Result = result;
            Pass = pass;
        }
		
    }

    public class ClassResult
    {
        public string Name {get; private set;}
        public List<MethodResult> MethodResults {get; private set;}
		
        public ClassResult(string name, List<MethodResult> methodResults)
        {
            Name = name;
            MethodResults = methodResults; 
        }
		
    }

    public class MetricResult
    {
        public string Name {get; private set;}
        public List<ClassResult> ClassResults {get; private set;}
		
        public MetricResult(string name, List<ClassResult> classResults)
        {
            Name = name;
            ClassResults = classResults;
			
        }
		
    }
}