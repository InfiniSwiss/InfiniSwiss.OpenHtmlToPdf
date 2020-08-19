using System;
using System.IO;

namespace OpenHtmlToPdf
{
    public sealed class TemporaryHtml : IDisposable
    {
        public TemporaryHtml(string html)
        {
            FileName = Path.Combine(PathProvider.BundledFilesDirectory(), $"{Guid.NewGuid()}.html");
            File.WriteAllText(FileName, html);
        }

        public string FileName { get; }

        public void Dispose()
        {
            File.Delete(FileName);
        }

    }
}
