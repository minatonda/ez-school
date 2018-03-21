using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Configuration.Jwt {

    public class JwtIssuerOptions {
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime IssuedAt => DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes (5);
        public DateTime Expiration => IssuedAt.Add (TimeSpan.FromDays(1));
        public Func<Task<string>> JtiGenerator => () => Task.FromResult (Guid.NewGuid ().ToString ());
        public SigningCredentials SigningCredentials { get; set; }
    }
}