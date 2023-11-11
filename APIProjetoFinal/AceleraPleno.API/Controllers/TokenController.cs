using AceleraPleno.API.Models;
using AceleraPleno.API.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AceleraPleno.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly IJWTAuthenticationManager _jwtAuthenticationManager;
        public TokenController(IJWTAuthenticationManager jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [AllowAnonymous]
        [HttpPost, Route("autenticar")]
        public IActionResult Authenticate([FromBody] TokenModel tokenM)
        {
            var token = _jwtAuthenticationManager.Authenticate(tokenM.clienteId, tokenM.clienteSecret);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost, Route("autenticarAngular")]
        public IActionResult AuthenticateAngular([FromBody] TokenModel tokenM)
        {
            TokenResponse token = new TokenResponse();
               token.Token = _jwtAuthenticationManager.Authenticate(tokenM.clienteId, tokenM.clienteSecret);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
    }
}
