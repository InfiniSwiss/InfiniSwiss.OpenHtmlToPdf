using System;
using System.IO;

namespace OpenHtmlToPdf
{
    public class TemporaryPdf : IDisposable
    {
        public TemporaryPdf(string temporaryFileName)
        {
            this.TemporaryFileName = temporaryFileName;
        }

        public string TemporaryFileName { get; }

        public static string TemporaryFilePath()
        {
            return Path.Combine(Path.GetTempPath(), "OpenHtmlToPdf", TemporaryFilename());
        }

        private static string TemporaryFilename()
        {
            return Guid.NewGuid().ToString("N") + ".pdf";
        }

        public void Dispose()
        {
            if (File.Exists(TemporaryFileName))
            {
                File.Delete(TemporaryFileName);
            }
        }
    }
}