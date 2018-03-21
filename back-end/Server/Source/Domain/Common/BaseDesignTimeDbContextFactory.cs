using System.IO;
using Configuration.Data;
using Domain.AreaInteresseDomain;
using Domain.CategoriaProfissionalDomain;
using Domain.CursoDomain;
using Domain.EnderecoDomain;
using Domain.InstituicaoDomain;
using Domain.MateriaDomain;
using Domain.UsuarioDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Domain.Common {

    public class BaseDesignTimeDbContextFactory : IDesignTimeDbContextFactory<BaseContext> {
        public BaseContext CreateDbContext(string[] args) {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<BaseContext>();

            var connectionString = configuration.GetSection(nameof(ConnectionStringOptions))[nameof(ConnectionStringOptions.BaseConnection)];

            builder.UseMySql(connectionString);

            return new BaseContext(builder.Options);
        }
    }
}