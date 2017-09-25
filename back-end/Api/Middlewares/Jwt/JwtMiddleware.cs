using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Api.Middlewares.Jwt {
    public static class JwtMiddleware {
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

        public static void AddJwtOptions (this IServiceCollection services, IConfiguration configuration, SymmetricSecurityKey signingKey, IHostingEnvironment environment) {
            IConfigurationSection jwtAppSettingOptions = configuration.GetSection (nameof (JwtIssuerOptions));
            var tokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof (JwtIssuerOptions.Audience)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof (JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };
            services
                .Configure<JwtIssuerOptions> (options => {
                    options.Issuer = jwtAppSettingOptions[nameof (JwtIssuerOptions.Audience)];
                    options.Audience = jwtAppSettingOptions[nameof (JwtIssuerOptions.Audience)];
                    options.SigningCredentials = new SigningCredentials (signingKey, SecurityAlgorithms.HmacSha256);
                })
                .AddAuthentication (options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer (o => {
                    // You also need to update /wwwroot/app/scripts/app.js
                    o.TokenValidationParameters = tokenValidationParameters;
                    //o.Authority = jwtAppSettingOptions[nameof (JwtIssuerOptions.Audience)];
                    o.Audience = jwtAppSettingOptions[nameof (JwtIssuerOptions.Audience)];
                    o.Events = new JwtBearerEvents () {
                        // OnAuthenticationFailed = c => {
                        //     c.NoResult ();
                        //     c.Response.StatusCode = 500;
                        //     c.Response.ContentType = "text/plain";
                        //     if (environment.IsDevelopment ()) {
                        //         // Debug only, in production do not share exceptions with the remote host.
                        //         return c.Response.WriteAsync (c.Exception.ToString ());
                        //     }
                        //     return c.Response.WriteAsync ("An error occurred processing your authentication.");
                        // }
                    };
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