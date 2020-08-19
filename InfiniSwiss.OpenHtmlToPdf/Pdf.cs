using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenHtmlToPdf
{
    public sealed class Pdf
    {
        public static IPdfDocument From(string html)
        {
            return DocumentBuilder.Containing(html);
        }

        private sealed class DocumentBuilder : IPdfDocument
        {
            private readonly string _html;
            private readonly IDictionary<string, string> _globalSettings;
            private readonly IDictionary<string, string> _objectSettings;

            private DocumentBuilder(string html, IDictionary<string, string> globalSettings, IDictionary<string, string> objectSettings)
            {
                _html = html;
                _globalSettings = globalSettings;
                _objectSettings = objectSettings;
            }

            public static DocumentBuilder Containing(string html)
            {
                return new DocumentBuilder(
                    html,
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>());
            }

            public IPdfDocument WithGlobalSetting(string key, string value)
            {
                var globalSettings = _globalSettings.ToDictionary(e => e.Key, e => e.Value);

                globalSettings[key] = value;

                return new DocumentBuilder(_html, globalSettings, _objectSettings);
            }

            public IPdfDocument WithObjectSetting(string key, string value)
            {
                var objectSetting = _objectSettings.ToDictionary(e => e.Key, e => e.Value);

                objectSetting[key] = value;

                return new DocumentBuilder(_html, _globalSettings, objectSetting);
            }

            public byte[] Content()
            {
                return ReadContentUsingTemporaryFile(TemporaryPdf.TemporaryFilePath());
            }

            public void ToFile(string fileName)
            {
                HtmlToPdfConverterProcess.ConvertToPdf(_html, fileName, _globalSettings, _objectSettings);
            }

            private byte[] ReadContentUsingTemporaryFile(string temporaryFilename)
            {

                using var temporaryFile = new TemporaryPdf(temporaryFilename);
                HtmlToPdfConverterProcess.ConvertToPdf(_html, temporaryFilename, _globalSettings, _objectSettings);

                if (!File.Exists(temporaryFilename))
                {
                    throw new PdfDocumentExportException();
                }
                return File.ReadAllBytes(temporaryFilename);
            }
        }
    }
}
