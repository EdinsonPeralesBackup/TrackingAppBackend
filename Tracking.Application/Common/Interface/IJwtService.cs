using System.Security.Claims;

namespace Tracking.Application.Common.Interface
{
    public interface IJwtService
    {
        string Generate(Claim[] claims, DateTime? experisUtc = null, string audience = null);
    }
}
