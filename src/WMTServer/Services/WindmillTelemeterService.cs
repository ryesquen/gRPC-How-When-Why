using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using WMTServer.Mock;

namespace WMTServer
{
    public class WindmillTelemeterService : WindmillTelemeter.WindmillTelemeterBase
    {
        private readonly ILogger<WindmillTelemeterService> _logger;
        private readonly WindmillsDataReader windmillsDataReader;

        public WindmillTelemeterService(ILogger<WindmillTelemeterService> logger, WindmillsDataReader windmillsDataReader)
        {
            _logger = logger;
            this.windmillsDataReader = windmillsDataReader;
        }

        public override Task<WindmillTelemetryResponse> RequestTelemetry(WindmillInfoRequest request, ServerCallContext context)
        {
            return Task.FromResult(windmillsDataReader.GetTelemetryValues(Guid.Parse(request.WindmillId)).LastOrDefault());

        }

        //public override Task<WindmillTelemetryResponse> SendTelemetry(WindmillInfoRequest request, ServerCallContext context)
        //{
        //    return Task.FromResult(new WindmillTelemetryResponse
        //    {
        //        Message = "Hello " + request.WindmillId
        //    });
        //}
    }
}
