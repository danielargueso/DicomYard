using FellowOakDicom;

namespace DicomYard.Infrastructure.Dicom.Shared;

internal static class SupportedTransferSyntaxes
{
    // Project: https://github.com/fo-dicom/fo-dicom-samples
    // File: https://raw.githubusercontent.com/fo-dicom/fo-dicom-samples/master/Desktop/C-Store%20SCP/Program.cs

    internal static readonly DicomTransferSyntax[] AcceptedTransferSyntaxes = new DicomTransferSyntax[]
    {
        DicomTransferSyntax.ExplicitVRLittleEndian,
        DicomTransferSyntax.ExplicitVRBigEndian,
        DicomTransferSyntax.ImplicitVRLittleEndian
    };

    internal static readonly DicomTransferSyntax[] AcceptedImageTransferSyntaxes = new DicomTransferSyntax[]
    {
        // Lossless
        DicomTransferSyntax.JPEGLSLossless,
        DicomTransferSyntax.JPEG2000Lossless,
        DicomTransferSyntax.JPEGProcess14SV1,
        DicomTransferSyntax.JPEGProcess14,
        DicomTransferSyntax.RLELossless,
        // Lossy
        DicomTransferSyntax.JPEGLSNearLossless,
        DicomTransferSyntax.JPEG2000Lossy,
        DicomTransferSyntax.JPEGProcess1,
        DicomTransferSyntax.JPEGProcess2_4,
        // Uncompressed
        DicomTransferSyntax.ExplicitVRLittleEndian,
        DicomTransferSyntax.ExplicitVRBigEndian,
        DicomTransferSyntax.ImplicitVRLittleEndian
    };
}
