
namespace Driven.Metrics.Reporting
{
    public interface IFileWriter
    {
        void Write(string filepath, string contents);
			
    }
}