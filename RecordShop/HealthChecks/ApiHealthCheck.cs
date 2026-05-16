using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RecordShop.Data;

namespace RecordShop.HealthChecks
{
    public class ApiHealthCheck :IHealthCheck
    {
        private readonly RecordShopDbContext _recordShopDbContext;

        public ApiHealthCheck(RecordShopDbContext recordShopDbContext)
        {
            _recordShopDbContext = recordShopDbContext;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {

            var album = _recordShopDbContext.Albums;

            

            int albumCount = await album.CountAsync();
            if (albumCount > 0)
            {
                return HealthCheckResult.Healthy($"There are {albumCount} albums available.");
            }
            else
            {
                return HealthCheckResult.Unhealthy($"There are {albumCount} albums available.");

            }
        }
    }
}
