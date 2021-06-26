using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory_Management.Server.APIHealthCheck
{
    public class CustomHealthCheck :IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,CancellationToken cancellationToken =new())
        {
            return Task.FromResult(HealthCheckResult.Healthy("OK"));
        }
    }
}
