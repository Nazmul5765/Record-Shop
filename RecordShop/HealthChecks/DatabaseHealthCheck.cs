using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RecordShop.Data;

namespace RecordShop.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly RecordShopDbContext _recordShopDbContext;

        public DatabaseHealthCheck(RecordShopDbContext recordShopDbContext)
        {
            _recordShopDbContext = recordShopDbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {

            bool canConnect = await _recordShopDbContext.Database.CanConnectAsync(cancellationToken);
            if (canConnect)
            {
                return HealthCheckResult.Healthy("Database connection is working.");
            }
            else
            {
                return HealthCheckResult.Unhealthy("Database connection failed.");

            }
        }
    }
}
