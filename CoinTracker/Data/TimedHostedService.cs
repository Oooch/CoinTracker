using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoinTracker.Data
{
    internal class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private IServiceProvider _services { get; }

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceProvider service)
        {
            _logger = logger;
            _services = service;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");

            using (var scope = _services.CreateScope())
            {
                //var DB = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var services = scope.ServiceProvider;
                Models.CoinAPI coinAPI = new Models.CoinAPI();
                coinAPI.BinanceUpdate(services);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
