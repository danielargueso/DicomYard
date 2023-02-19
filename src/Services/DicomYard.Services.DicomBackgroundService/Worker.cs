using DicomYard.Infrastructure.Dicom.DicomServices.DicomEcho;
using FellowOakDicom.Network;

namespace DicomYard.Services.DicomBackgroundService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var server = DicomServerFactory.Create<CEchoSCP>(104);
            
            _logger.LogInformation("Listening in: {ip}:{port} ({active})", server.IPAddress, server.Port, server.IsListening);
            if (server.Exception != null)
            {
                _logger.LogError(server.Exception.Message);
            }
            while(!stoppingToken.IsCancellationRequested)
            {

            }
        }
    }
}