namespace Nsu.Contest.Web.HRDirector.Services;

using MassTransit;
using Microsoft.Extensions.Options;
using Nsu.Contest.Web.Common.Message;
using Nsu.Contest.Web.HRDirector.Model;

public class HRDirectorProducer(IOptions<HRDirectorConfig> _config, IBus _bus) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        for (int i = 0; i < _config.Value.ContestCount; i++)
        {
            await _bus.Publish(new ContestStartMessage{Id = Guid.NewGuid()});
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}