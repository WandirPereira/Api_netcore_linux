using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interface;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();

            //serviceCollection.AddDbContext<MyContext>(
            //options => options.UseSqlServer("Server=localhost;Database=dbapi;User ID=sa;Password=senha@1234;Encrypt=False"));
            // serviceCollection.AddDbContext<MyContext>(
            //     options => options.UseMySql("Server=localhost;Port=3306;DataBase=dbAPI;Uid=root;Pwd=senha@1234", new MySqlServerVersion(new Version(8, 0, 31))));

            if (Environment.GetEnvironmentVariable("DATABASE").ToLower() == "SQLSERVER".ToLower())
            {
                serviceCollection.AddDbContext<MyContext>(
                    options => options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION_SQLSERVER"))
                );
            }
            else
            {
                serviceCollection.AddDbContext<MyContext>(
                options => options.UseMySql(Environment.GetEnvironmentVariable("DB_CONNECTION_MYSQL"),
                    new MySqlServerVersion(new Version(8, 0, 21))));
            }

        }
    }
}
