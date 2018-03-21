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
    [Route("api/login")]
    public class LoginController : Controller {

        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger _logger;
        private UsuarioRepository _usuarioRepository;

        public LoginController(ILoggerFactory loggerFactory, IOptions<JwtIssuerOptions> jwtOptions, UsuarioRepository usuarioRepository) {
            _logger = loggerFactory.CreateLogger<LoginController>();
            _jwtOptions = jwtOptions.Value;
            this._usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ObjectResult> Post([FromBody] UsuarioVM vm) {
            var user = UsuarioAdapter.ToModel(vm, true);

            ClaimsIdentity identity = await GetClaimsIdentity(user);

            if (identity == null) {
                _logger.LogInformation($"Invalid email ({user.Username}) or password ({user.Password})");
                return BadRequest("Invalid credentials");
            }

            Claim[] claims = new Claim[5] {
                new Claim (JwtRegisteredClaimNames.Sub, user.Username),
                new Claim (JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator ()),
                new Claim (JwtRegisteredClaimNames.Iat, _jwtOptions.IssuedAt.ToUnixEpochDateToString (), ClaimValueTypes.Integer64),
                identity.FindFirst ("Auth"),
                identity.FindFirst ("usuario-info-id")
            };

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler()
                .WriteToken(jwt);

            var response = new AutenticacaoVM() {
                access_token = encodedJwt,
                created = _jwtOptions.IssuedAt,
                expires = _jwtOptions.Expiration,
                time_zone = TimeZoneInfo.Local.StandardName
            };

            return Ok(response);
        }

        private Task<ClaimsIdentity> GetClaimsIdentity(Usuario user) {
            var attachedUser = this._usuarioRepository.Query(m => m.Username == user.Username && m.Password == m.Password).SingleOrDefault();
            if (attachedUser != null) {
                return Task.FromResult(new ClaimsIdentity(
                    new GenericIdentity(user.Username, "Token"),
                    new[] {
                        new Claim ("usuario-info-id", attachedUser.ID),
                        new Claim ("Auth", "WebApi")
                    }
                ));
            }
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}