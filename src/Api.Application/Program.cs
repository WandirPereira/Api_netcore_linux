using Api.CrossCutting.DependencyInjection;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
