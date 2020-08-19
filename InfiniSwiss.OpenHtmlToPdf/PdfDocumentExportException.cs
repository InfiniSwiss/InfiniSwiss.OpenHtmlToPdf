using System;
using System.Runtime.Serialization;

namespace OpenHtmlToPdf
{
    public sealed class PdfDocumentExportException : Exception
    {
        public PdfDocumentExportException()
        {
        }

        public PdfDocumentExportException(string message) : base(message)
        {
        }

        public PdfDocumentExportException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
