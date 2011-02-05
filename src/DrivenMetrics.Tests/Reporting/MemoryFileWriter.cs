using Driven.Metrics.Reporting;

namespace Driven.Metrics.Tests.Reporting
{
    internal class MemoryFileWriter : IFileWriter
    {
        public string FilePath { get; private set; }
        public string Contents { get; private set; }

        #region IFileWriter Members

        public void Write (string filepath, string contents)
        {
            FilePath = filepath;
            Contents = contents;
        }

        #endregion
    }
}
