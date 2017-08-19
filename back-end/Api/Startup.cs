using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Models.Identity;
using Api.Providers.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api {
    public class Startup {
        public Startup (IHostingEnvironment env) {
            var builder = new ConfigurationBuilder ()
                .SetBasePath (env.ContentRootPath)
                .AddJsonFile ("appsettings.json", optional : false, reloadOnChange : true)
                .AddJsonFile ($"appsettings.{env.EnvironmentName}.json", optional : true)
                .AddEnvironmentVariables ();
            Configuration = builder.Build ();
        }

        public IConfigurationRoot Configuration { get; }
        //28 caracteres
        public const string SecretKey = "a1234567891012141516182025262b";
        public readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey (Encoding.ASCII.GetBytes (SecretKey));

        public void ConfigureServices (IServiceCollection services) {
            services.AddEntityFrameworkSqlServer ();
            //services.AddMvc();
            services.AddMvcWithPolicy ();
            services.AddJwtOptions (Configuration, SigningKey);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            loggerFactory.AddConsole (Configuration.GetSection ("Logging"));
            loggerFactory.AddDebug ();
            app.UseRequestLocalizationFromBrazil ();
            app.UseAuthentication ();
            app.UseMvc ();
        }

    }
}