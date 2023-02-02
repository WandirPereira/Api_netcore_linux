using Api.CrossCutting.DependencyInjection;
using Api.Data.Context;
using Api.Domain.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Environment.SetEnvironmentVariable("DB_CONNECTION", "Persist Security Info=True;Server=localhost;Port=3306;DataBase=dbAPI_NET6_Integration;Uid=root;Pwd=mudar@123");
Environment.SetEnvironmentVariable("DATABASE", "MYSQL");
Environment.SetEnvironmentVariable("MIGRATION", "APLICAR");
Environment.SetEnvironmentVariable("Audience", "ExemploAudience");
Environment.SetEnvironmentVariable("Issuer", "ExemploIssue");
Environment.SetEnvironmentVariable("Seconds", "28800");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "API NET 6 Linux",
            Description = "Criando uma API NET Core 6 no Linux Ubuntu 22.04",
            TermsOfService = new Uri("http://www.google.com"),
            Contact = new OpenApiContact
            {
                Name = "Wandir Pereira Filho",
                Email = "wandir@gmail.com"
            },
            License = new OpenApiLicense
            {
                Name = "Termo de Licença de Uso",
                Url = new Uri("http://www.google.com")
            }
        });
    });
ConfigureService.ConfigureDependenciesService(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services);

var signingConfigurations = new SigningConfigurations();
builder.Services.AddSingleton(signingConfigurations);

//var tokenConfiguration = new TokenConfiguration();
// new ConfigureFromConfigurationOptions<TokenConfiguration>(
//     Configuration("TokenConfiguration").Configure(tokenConfiguration);
// )

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Net 6 Linux");
            c.RoutePrefix = string.Empty;
        }
    );
}

app.UseAuthorization();

app.MapControllers();

app.Run();
