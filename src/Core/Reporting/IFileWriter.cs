
namespace DrivenMetrics.Reporting
{
    public interface IFileWriter
    {
        void Write(string filepath, string contents);
			
    }
}