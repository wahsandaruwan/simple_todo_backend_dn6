// Local directives
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // -----Auth instance-----
        private readonly IConfiguration _config;

        // -----Constructor-----
        public AuthController(IConfiguration config){
            _config = config;
        }

        // -----All utilities-----
        // Authenticate user
        private User AuthenticateUser(User user){
            User? _user = null;
            if(user.UserName == "admin"){
                _user = new User {UserName = "Kamal Silva"};
            }
            return _user;
        }

        // Generate token
        private string GenerateToken(User user){
            // Create security key and credentials
            var security_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(security_key, SecurityAlgorithms.HmacSha256);

            // Generate token
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null, expires: DateTime.Now.AddMinutes(1), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}