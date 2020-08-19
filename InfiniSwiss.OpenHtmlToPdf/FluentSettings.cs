using System;
using System.Globalization;

namespace OpenHtmlToPdf
{
    public static class FluentSettings
    {
        public static IPdfDocument Compressed(this IPdfDocument pdfDocument)
        {
            return pdfDocument.WithGlobalSetting("useCompression", "true");
        }

        public static IPdfDocument WithTitle(this IPdfDocument pdfDocument, string title)
        {
            return pdfDocument.WithGlobalSetting("title", title);
        }

        public static IPdfDocument WithOutline(this IPdfDocument pdfDocument)
        {
            return pdfDocument.WithGlobalSetting("outline", null);
        }

        public static IPdfDocument WithoutOutline(this IPdfDocument pdfDocument)
        {
            return pdfDocument.WithGlobalSetting("no-outline", null);
        }

        public static IPdfDocument EncodedWith(this IPdfDocument pdfDocument, string encoding)
        {
            return pdfDocument.WithObjectSetting("web.defaultEncoding", encoding);
        }

        public static IPdfDocument Landscape(this IPdfDocument pdfDocument)
        {
            return pdfDocument
                .WithGlobalSetting("orientation", "Landscape");
        }

        public static IPdfDocument Portrait(this IPdfDocument pdfDocument)
        {
            return pdfDocument
                .WithGlobalSetting("orientation", "Portrait");
        }

        public static IPdfDocument OfSize(this IPdfDocument pdfDocument, PaperSize paperSize)
        {
            return pdfDocument
                .WithGlobalSetting("page-size", paperSize.ToString());
        }

        public static IPdfDocument WithMargins(this IPdfDocument pdfDocument, Func<PaperMargins,PaperMargins>  paperMargins)
        {
            return pdfDocument.WithMargins(paperMargins(PaperMargins.None()));
        }

        public static IPdfDocument WithMargins(this IPdfDocument pdfDocument, PaperMargins margins)
        {
            return pdfDocument
                .WithGlobalSetting("margin-bottom", margins.BottomSetting)
                .WithGlobalSetting("margin-left", margins.LeftSetting)
                .WithGlobalSetting("margin-right", margins.RightSetting)
                .WithGlobalSetting("margin-top", margins.TopSetting);
        }

        public static IPdfDocument WithResolution(this IPdfDocument pdfDocument, int dpi)
        {
            return pdfDocument
                .WithGlobalSetting("dpi", dpi.ToString(CultureInfo.InvariantCulture));
        }
    }
}