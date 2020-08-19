using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenHtmlToPdf
{
    public class PathProvider
    {
        private const string ConverterExecutableFilename = "wkhtmltopdf.exe";

        public static string ResolveFullPathToConverterExecutableFile()
        {
            return Path.Combine(BundledFilesDirectory(), ConverterExecutableFilename);
        }

        public static string BundledFilesDirectory()
        {
            var directory= Path.Combine(Path.GetTempPath(), "OpenHtmlToPdf", Version());
            Directory.CreateDirectory(directory);
            return directory;
        }

        public static string Version()
        {
            return string.Format("{0}_{1}",
                Assembly.GetExecutingAssembly().GetName().Version,
                Environment.Is64BitProcess ? 64 : 32);
        }
    }
}
