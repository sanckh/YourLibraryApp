using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace YourLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IUserService userService;
        private readonly IConfiguration configuration;
        private readonly ILogger<UserController> logger;

        public AccountController(IAccountService _acc, IConfiguration _con, IUserService _user, ILogger<UserController> logger)
        {
            accountService = _acc;
            configuration = _con;
            userService = _user;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginRequestModel loginRequest)
        {
            var user = await accountService.Validate(loginRequest.Email, loginRequest.Password);
            if(user == null)
            {
                return Unauthorized("Invalid Username or Password");
            }
            var token = GenerateJWT(user);
            var tokenValue = new { jwt = token };
            return Ok(tokenValue);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            try
            {
                var result = await accountService.Register(model);
                var response = new { Message = "Registered Successfully" };
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequestModel request)
        {
            var principal = GetPrincipalFromExpiredToken(request.Token);
            var newToken = GenerateJWTFromClaims(principal.Claims);

            return Ok(newToken);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Issuer"],
                ValidAudience = configuration["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["PrivateKey"])),
                ValidateLifetime = false // Here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        private string GenerateJWTFromClaims(IEnumerable<Claim> claims)
        {
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["PrivateKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expire = DateTime.UtcNow.AddHours(Convert.ToDouble(configuration["ExpirationHours"]));
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = expire,
                SigningCredentials = credentials,
                Issuer = configuration["Issuer"],
                Audience = configuration["Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }


        private string GenerateJWT(UserLoginModel model)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, model.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, model.LastName),
                new Claim(JwtRegisteredClaimNames.Email, model.Email),
                new Claim("language", "english"),
            };

            if (model.DateOfBirth.HasValue)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Birthdate, model.DateOfBirth.Value.ToShortDateString()));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["PrivateKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expire = DateTime.UtcNow.AddHours(Convert.ToDouble(configuration["ExpirationHours"]));
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = expire,
                SigningCredentials = credentials,
                Issuer = configuration["Issuer"],
                Audience = configuration["Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescription);

            // Decode the generated token to inspect the claims
            var decodedToken = tokenHandler.ReadJwtToken(tokenHandler.WriteToken(token));
            var userId = decodedToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var firstName = decodedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.GivenName)?.Value;
            var lastName = decodedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.FamilyName)?.Value;
            var email = decodedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
            var dateOfBirth = decodedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Birthdate)?.Value;

            // Log the claims for debugging purposes
            logger.LogInformation($"Decoded Token - UserId: {userId}, FirstName: {firstName}, LastName: {lastName}, Email: {email}, DateOfBirth: {dateOfBirth}");

            return tokenHandler.WriteToken(token);
        }
    }
}
