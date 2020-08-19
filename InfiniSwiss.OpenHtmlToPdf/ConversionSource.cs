using System.Collections.Generic;

namespace OpenHtmlToPdf
{
    class ConversionSource
    {
        public string Html { get; set; }
        public string PdfFile { get; set; }
        public IDictionary<string, string> GlobalSettings { get; set; }
        public IDictionary<string, string> ObjectSettings { get; set; }
    }
}
