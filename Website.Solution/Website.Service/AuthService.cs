using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Website.Core;
using Website.Core.Entities.Identity;
using Website.Core.ServiceInterfaces;

namespace Website.Service;

public class AuthService : IAuthService
{
	private readonly IConfiguration _configuration;
	private readonly IUnitOfWork _unitOfWork;

	public AuthService(IConfiguration configuration , IUnitOfWork unitOfWork)
	{
		_configuration=configuration;
		_unitOfWork=unitOfWork;
	}
	public async Task<JwtSecurityToken> CreateTokenAsync(List<Claim> authClaims)
	{

		var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"] !));

		var token = new JwtSecurityToken
		(
			audience: _configuration["JWT:Audience"],
			issuer: _configuration["JWT:Issuer"],
			expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:ExpireInDays"] !)),
			claims: authClaims,
			signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
		);

		return token;
	}

	public async Task<RefreshToken> GenerateRefreshToken(ApplicationUser user)
	{
		var refreshToken = new RefreshToken
		{
			UserId = user.Id,
			Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
			Expires = DateTime.UtcNow.AddDays(7),
			Created = DateTime.UtcNow,
			IsRevoked = false
		};

		_unitOfWork.Repository<RefreshToken>().Add(refreshToken);

		await _unitOfWork.CompleteAsync();

		return refreshToken;
	}
}
