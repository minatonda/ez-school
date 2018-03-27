using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Configuration.Jwt;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.UsuarioApi {

    [Produces("application/json")]
    [Route("api")]
    public class LoginController : Controller {

        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger _logger;
        private UsuarioService _usuarioService;

        public LoginController(ILoggerFactory loggerFactory, IOptions<JwtIssuerOptions> jwtOptions, UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository) {
            _logger = loggerFactory.CreateLogger<LoginController>();
            _jwtOptions = jwtOptions.Value;
            this._usuarioService = new UsuarioService(usuarioRepository, areaInteresseRepository);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<AutenticacaoVM> Auth([FromBody] UsuarioVM vm) {

            var usuarioAutenticado = this._usuarioService.Autenticar(vm);

            ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuarioAutenticado.Username, "Token"),
                    new[] {
                        new Claim (JwtRegisteredClaimNames.Sub, usuarioAutenticado.Username),
                        new Claim (JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator ()),
                        new Claim (JwtRegisteredClaimNames.Iat, _jwtOptions.IssuedAt.ToUnixEpochDateToString (), ClaimValueTypes.Integer64),
                        new Claim ("IdUsuarioInfo", usuarioAutenticado.ID),
                        new Claim ("Auth", "WebApi")
                    }
            );

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: identity.Claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AutenticacaoVM() {
                access_token = encodedJwt,
                created = _jwtOptions.IssuedAt,
                expires = _jwtOptions.Expiration,
                time_zone = TimeZoneInfo.Local.StandardName
            };

        }

    }
}