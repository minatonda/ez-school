using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Configuration;
using Api.Data;
using Api.Middlewares.Jwt;
using Domain;
using Domain.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api {
    public class Startup {
        public Startup (IHostingEnvironment env) {
            var builder = new ConfigurationBuilder ()
                .SetBasePath (env.ContentRootPath);
            if (env.IsDevelopment ()) {
                builder.AddUserSecrets<Startup> ();
            }

            Environment = env;
            Configuration = builder
                .AddJsonFile ("appsettings.json", optional : false, reloadOnChange : true)
                .AddJsonFile ($"appsettings.{env.EnvironmentName}.json", optional : true)
                .AddEnvironmentVariables ()
                .Build ();

            connectionStringOptions = Configuration.GetSection (nameof (ConnectionStringOptions));
        }

        //28 caracteres
        public const string SecretKey = "a1234567891012141516182025262b";
        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment Environment { get; set; }
        public readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey (Encoding.ASCII.GetBytes (SecretKey));
        public IConfigurationSection connectionStringOptions;
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<BaseContext> (opt => opt.UseInMemoryDatabase ("devDB"));
            // services.AddDbContext<BaseContext>(options =>
            //     options.UseMySql(connectionStringOptions[nameof(ConnectionStringOptions.BaseConnection)])
            // );

            services.AddCors ();
            services.AddMvcWithPolicy ();

            services.AddScoped<BaseContext, BaseContext> ();
            services.AddTransient<CursoRepository, CursoRepository> ();
            services.AddTransient<MateriaRepository, MateriaRepository> ();
            services.AddTransient<ProfessorRepository, ProfessorRepository> ();
            services.AddTransient<InstituicaoRepository, InstituicaoRepository> ();
            services.AddTransient<InstituicaoCategoriaRepository, InstituicaoCategoriaRepository> ();
            services.AddTransient<UsuarioRepository, UsuarioRepository> ();

            services.AddJwtOptions (Configuration, SigningKey, Environment);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, BaseContext context) {
            if (Environment.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            loggerFactory.AddConsole (Configuration.GetSection ("Logging"));
            loggerFactory.AddDebug ();

            app.UseRequestLocalizationFromBrazil ();
            app.UseAuthentication ();
            app.UseCors (builder =>
                builder.WithOrigins ("http://localhost:8080")
                .WithOrigins ("http://192.168.1.36:8080")
                .AllowAnyHeader ().AllowAnyMethod ()
            );
            app.UseMvc ();

            BaseContextInitializer.Initialize (context);
        }

    }
}