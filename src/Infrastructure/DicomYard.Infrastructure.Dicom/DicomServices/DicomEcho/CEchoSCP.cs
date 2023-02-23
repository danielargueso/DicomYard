using DicomYard.Infrastructure.Dicom.Shared;
using FellowOakDicom;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using System.Text;
using System.Text.Json;

namespace DicomYard.Infrastructure.Dicom.DicomServices.DicomEcho;

public class CEchoSCP : DicomService, IDicomServiceProvider, IDicomCEchoProvider
{
    private static readonly string[] _allowedCalledAETs = { "CALLEDAET01", "CALLEDAET02" };
    private static readonly string[] _allowedCallingAETs = { "CALLINGAET01", "CALLINGAET02" };
    private readonly ILogger _logger;

    public CEchoSCP(INetworkStream stream, Encoding fallbackEncoding, ILogger logger, DicomServiceDependencies dependencies)
        : base(stream, fallbackEncoding, logger, dependencies)
    {
        _logger = logger;
    }
    public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
    {
        var associationDump = JsonSerializer.Serialize(association);
        _logger.Warn("OnReceiveAssociationRequestAsync");
        _logger.Warn("{0}", associationDump);

        if (!_allowedCalledAETs.Contains(association.CalledAE))
        {
            return SendAssociationRejectAsync(
                result: DicomRejectResult.Permanent,
                source: DicomRejectSource.ServiceUser,
                reason: DicomRejectReason.CalledAENotRecognized
                );
        }

        if (!_allowedCallingAETs.Contains(association.CallingAE))
        {
            return SendAssociationRejectAsync(
                result: DicomRejectResult.Permanent,
                source: DicomRejectSource.ServiceUser,
                reason: DicomRejectReason.CallingAENotRecognized
                );
        }

        foreach (var pc in association.PresentationContexts)
        {
            if (pc.AbstractSyntax == DicomUID.Verification)
            {
                pc.AcceptTransferSyntaxes(SupportedTransferSyntaxes.AcceptedTransferSyntaxes);
            }
        }

        return SendAssociationAcceptAsync(association);
    }

    public Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request)
    {
        _logger.Warn("OnCEchoRequestAsync");

        return Task.FromResult(new DicomCEchoResponse(request, DicomStatus.Success));

    }
    public Task OnReceiveAssociationReleaseRequestAsync()
    {
        return SendAssociationReleaseResponseAsync();
    }
    public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
    {
        var sourceDump = JsonSerializer.Serialize(source)
            .Replace("{", "¿").Replace("}", "?");
        var reasonDump = JsonSerializer.Serialize(reason)
            .Replace("{", "¿").Replace("}", "?");
        
        _logger.Warn("OnReceiveAbort");
        _logger.Warn(sourceDump);
        _logger.Warn(reasonDump);
    }

    public void OnConnectionClosed(Exception exception)
    {
        _logger.Warn("OnConnectionClosed");
        var exceptionDump = JsonSerializer.Serialize(exception)
            .Replace("{", "¿").Replace("}", "?");
        _logger.Warn(exceptionDump);
    }
}
