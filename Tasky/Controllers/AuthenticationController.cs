using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tasky.Dtos.Request;
using Tasky.Interfaces.Services;

namespace Tasky.Controllers;

[ApiController]
public class AuthenticationController : TaskyApiBaseControllerV1
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;

    public AuthenticationController(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Login(LoginRequestDto loginDto)
    {
        var isValid = await _authService.ValidateCredentials(loginDto.Email, loginDto.Password);
        if (!isValid)
        {
            return Unauthorized();
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, loginDto.Email),
        };

        var issuer = _configuration.GetValue<string>("Jwt:Issuer");
        var audience = _configuration.GetValue<string>("Jwt:Audience");
        var validationKey = _configuration.GetValue<string>("Jwt:Key");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(validationKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
