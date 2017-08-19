using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Api.Providers.Jwt {
    public static class JwtProvider {
        public static void AddMvcWithPolicy (this IServiceCollection services) {
            services.AddMvc (config => {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder ()
                    .RequireAuthenticatedUser ()
                    .Build ();
                config.Filters.Add (new AuthorizeFilter (policy));
            });

            services.AddAuthorization (options => {
                options.AddPolicy ("UserApi", policy => policy.RequireClaim ("Auth", "WebApi"));
            });
            
        }

        public static void AddJwtOptions (this IServiceCollection services, IConfiguration configuration, SymmetricSecurityKey signingKey) {
            IConfigurationSection jwtAppSettingOptions = configuration.GetSection (nameof (JwtIssuerOptions));
            var tokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof (JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof (JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            //CookieAuthenticationDefaults.AuthenticationScheme)
            services
                .AddAuthentication (o => {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                //.AddCookie (o => o.LoginPath = new PathString ("/login"))
                .AddJwtBearer (o => {
                    o.TokenValidationParameters = tokenValidationParameters;
                    o.Audience = jwtAppSettingOptions[nameof (JwtIssuerOptions.Audience)];
                });
        }

        public static void UseRequestLocalizationFromBrazil (this IApplicationBuilder app) {
            app.UseRequestLocalization (new RequestLocalizationOptions {
                DefaultRequestCulture = new RequestCulture ("pt-BR", "pt-BR")
            });
        }

        public static long ToUnixEpochDate (this DateTime date) {
            return (long) Math.Round ((date.ToUniversalTime () - new DateTimeOffset (1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }

        public static string ToUnixEpochDateToString (this DateTime date) {
            return $"{ToUnixEpochDate(date)}";
        }

    }
}