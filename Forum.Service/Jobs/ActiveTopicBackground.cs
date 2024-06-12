using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Forum.Data;
using Microsoft.EntityFrameworkCore;
using Forum.Entities;

namespace Forum.Service.Jobs
{
    public class ActiveTopicBackground : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ActiveTopicBackground> _logger;
        private Timer _timer;
        private readonly int _inactivityDays;

        public ActiveTopicBackground(IServiceProvider serviceProvider, ILogger<ActiveTopicBackground> logger, int inactivityDays)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _inactivityDays = inactivityDays;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ActiveTopicBackground Service is starting.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(24));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("ActiveTopicBackground Service is working.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var cutoffDate = DateTime.UtcNow.AddDays(-_inactivityDays);

                var inactiveTopics = dbContext.Topics
                    .Include(t => t.Comments)
                    .Where(t => t.Comments.Any() && t.Comments.Max(c => c.CreationDate) < cutoffDate && t.Status != Status.Inactive)
                    .ToList();

                foreach (var topic in inactiveTopics)
                {
                    topic.Status = Status.Inactive;
                }

                dbContext.SaveChanges();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ActiveTopicBackground Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
