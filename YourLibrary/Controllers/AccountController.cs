using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IConfiguration configuration;
        public AccountController(IAccountService _acc, IConfiguration _con)
        {
            accountService = _acc;
            configuration = _con;
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

        private string GenerateJWT(UserLoginModel model)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, model.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, model.LastName),
                new Claim(JwtRegisteredClaimNames.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Birthdate, model.DateOfBirth.ToShortDateString()),
                new Claim("language", "english")
            };
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
    }
}
