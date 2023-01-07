using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Delivery_Report_System.Models;
using Delivery_Report_System.Models.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace Delivery_Report_System.Services;

public class JwtService
{
    private readonly IConfiguration _configuration;
    private const int EXPIRATION_MINUTES = 10;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Response CreateToken(ApplicationUser user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(EXPIRATION_MINUTES);
        var token = CreateJwtToken(CreateClaims(user),
            CreateSigningCredentials(),
            expiration);

        var tokenHandler = new JwtSecurityTokenHandler();
        return new Response()
        {
            Token = tokenHandler.WriteToken(token),
            Expiration = expiration
        };
    }
    
    private JwtSecurityToken CreateJwtToken(Claim[] claims,
        SigningCredentials credentials,
        DateTime expiration)=>
    new JwtSecurityToken(
        _configuration["Jwt:Issuer"],
        _configuration["Jwt:Audience"],
        claims,
        expires:expiration,
        signingCredentials: credentials);


    private Claim[] CreateClaims(ApplicationUser user) =>
        new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.Name,user.Email),
            new Claim(ClaimTypes.Name,user.Role)
        };
    
    private SigningCredentials CreateSigningCredentials()=>
    new SigningCredentials(
        new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
        SecurityAlgorithms.HmacSha256
            );

}