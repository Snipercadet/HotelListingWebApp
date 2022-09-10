using HotelListing.Models;
using HotelListing.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelListing.Configuration.Services
{
    public class TokenService
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _config;
        //private ApiUser _user;
        public TokenService(UserManager<ApiUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
          
        }

        public async Task<string> GenerateToken(ApiUser apiUser)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, apiUser.Email),
                new Claim(ClaimTypes.Name, apiUser.UserName)

            };

            var roles = await _userManager.GetRolesAsync(apiUser);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = Environment.GetEnvironmentVariable("KEY");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha512);

            var expires = DateTime.Now.AddDays(1);
            var TokenOptions = new JwtSecurityToken(
                    issuer: _config.GetSection("Issuer").Value,
                    claims: claims,
                    signingCredentials: credentials,
                    expires: expires
                 );
            return new JwtSecurityTokenHandler().WriteToken(TokenOptions);
        }
        //public async Task<string> CreateToken()
        //{
        //    var signingCredentials = GetSigningCredentials();
        //    var claims = GetClaims();
        //    var token = GenerateTokenOptions(signingCredentials, await claims);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        //{
        //    var jwtSettings = _configuration.GetSection("jwt");
        //    var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("lifetime").Value));
        //    var token = new JwtSecurityToken(
        //        issuer: jwtSettings.GetSection("validIssuer").Value,
        //       claims: claims,
        //         expires: expiration,
        //        signingCredentials: signingCredentials
        //        );

        //    return token;
        //}

        //private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        //{
        //    var jwtSettings = _configuration.GetSection("jwt");

        //    var token = new JwtSecurityToken(
        //        issuer: jwtSettings.GetSection("validIssuer").Value,
        //        claims: claims,
        //        expires: jwtSettings.GetSection("lifetime").Value,
        //        signingCredentials: signingCredentials
        //        );

        //    return token;
        //}

                     

        //private async Task<List<Claim>> GetClaims()
        //{
        //    var claims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.Name, _user.UserName)
        //    };

        //    var roles = await _userManager.GetRolesAsync(_user);
        //    foreach (var role in roles)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    }
        //    return claims;
        //}

        //private static SigningCredentials GetSigningCredentials()
        //{
        //    var key = Environment.GetEnvironmentVariable("KEY");
        //    var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        //    return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        //}

        //public async Task<bool> ValidateUser(LoginDTO loginDTO)
        //{
        //    _user = await _userManager.FindByNameAsync(loginDTO.Email);
        //    return (_user != null && await _userManager.CheckPasswordAsync(_user, loginDTO.Password));

        //}
    }
}
