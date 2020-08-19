using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OpenHtmlToPdf.Assets;

namespace OpenHtmlToPdf
{
    static class HtmlToPdfConverterProcess
    {
        public static void ConvertToPdf(
            string html,
            string pdfFile,
            IDictionary<string, string> globalSettings,
            IDictionary<string, string> objectSettings)
        {
            Convert(ToConversionSource(html, pdfFile, globalSettings, objectSettings));
        }

        private static void Convert(ConversionSource conversionSource)
        {
            using var file = new TemporaryHtml(conversionSource.Html);
            var processStartInfo = GetProcessStartInfo(conversionSource, file);
            using var process = Process.Start(processStartInfo);
            process.WaitForExit();
            var a = process.ExitCode;
        }

        private static ProcessStartInfo GetProcessStartInfo(ConversionSource conversionSource, TemporaryHtml file)
        {
            return new ProcessStartInfo
            {
                FileName = ConverterExecutable.Get().FullConverterExecutableFilename,
                Arguments = $"{string.Join(" ", conversionSource.GlobalSettings.Select(s => $"--{s.Key} {(s.Value != null ? $"\"{s.Value}\"" : string.Empty)}"))} \"{file.FileName}\" \"{conversionSource.PdfFile}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            };
        }

        private static ConversionSource ToConversionSource(string html, string pdfFile, IDictionary<string, string> globalSettings, IDictionary<string, string> objectSettings)
        {
            var conversionSource = new ConversionSource
            {
                Html = html,
                PdfFile = pdfFile,
                GlobalSettings = globalSettings,
                ObjectSettings = objectSettings
            };
            return conversionSource;
        }
    }
}