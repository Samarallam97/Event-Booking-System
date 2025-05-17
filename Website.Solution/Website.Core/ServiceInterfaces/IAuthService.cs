using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Website.Core.Entities.Identity;

namespace Website.Core.ServiceInterfaces;

public interface IAuthService
{
	Task<JwtSecurityToken> CreateTokenAsync(List<Claim> claims);
	Task<RefreshToken> GenerateRefreshToken(ApplicationUser user);
}
