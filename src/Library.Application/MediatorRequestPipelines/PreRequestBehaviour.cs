using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Library.Application.MediatRRequestPipelines
{
	public class PreRequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<TRequest> _logger;

        public PreRequestLogger(ILogger<TRequest> logger)
            => _logger = logger;

        public Task Process(TRequest request, CancellationToken cancellationToken = default)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogInformation("Request started: {requestName}", requestName);

            return Task.CompletedTask;
        }
    }
}
