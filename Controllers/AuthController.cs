// Local directives
using System;
using System.Collections.Generic;
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
    }
}