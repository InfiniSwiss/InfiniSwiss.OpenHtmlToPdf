using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace OpenHtmlToPdf.Assets
{
    sealed class ConverterExecutable
    {
        static readonly object lockFileObject = new object();

        private ConverterExecutable()
        {
        }

        public static ConverterExecutable Get()
        {
            var bundledFile = new ConverterExecutable();

            bundledFile.CreateIfConverterExecutableDoesNotExist();

            return bundledFile;
        }

        public string FullConverterExecutableFilename
        {
            get { return PathProvider.ResolveFullPathToConverterExecutableFile(); }
        }

        private void CreateIfConverterExecutableDoesNotExist()
        {
            lock(lockFileObject)
            {
                if (!File.Exists(FullConverterExecutableFilename))
                {
                    ExtractExecutableContent();
                }
            }
        }

        private static void ExtractExecutableContent()
        {
            Directory.CreateDirectory(PathProvider.BundledFilesDirectory());

            using var zipArchive = new ZipArchive(GetConverterExecutable());
            var enumerator = zipArchive.Entries.GetEnumerator();
            enumerator.MoveNext();
            var zipEntry = enumerator.Current;
            zipEntry.ExtractToFile(PathProvider.ResolveFullPathToConverterExecutableFile());
        }

        private static Stream GetConverterExecutable()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OpenHtmlToPdf.Assets.wkhtmltopdf.zip");
        }

    }
}