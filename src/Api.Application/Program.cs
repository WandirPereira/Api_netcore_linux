using Api.CrossCutting.DependencyInjection;
using Api.CrossCutting.DtoMappings;
using Api.Data.Context;
using Api.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;

// Add services to the container.
//if (environment.IsEnvironment("Development"))
//{
Environment.SetEnvironmentVariable("DB_CONNECTION_SQLSERVER", "Server=localhost;Database=dbapi;User ID=sa;Password=senha@1234;Encrypt=False");
Environment.SetEnvironmentVariable("DB_CONNECTION_MYSQL", "Server=localhost;Port=3306;DataBase=dbAPI;Uid=root;Pwd=senha@1234");
Environment.SetEnvironmentVariable("DATABASE", "SQLSERVER");  //MYSQL OR SQLSERVER
Environment.SetEnvironmentVariable("MIGRATION", "NAO_APLICAR");
Environment.SetEnvironmentVariable("Audience", "ExemploAudience");
Environment.SetEnvironmentVariable("Issuer", "ExemploIssue");
Environment.SetEnvironmentVariable("Seconds", "28800");
//}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "API NET 7 Linux",
            Description = "Criando uma API NET Core 7 no Linux Ubuntu 22.04",
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

        //Adicionando botão Authorize no Swagger
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Entre com o Token JWT",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement{
            {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                }, new List<string>()
            }
        });
    });
ConfigureService.ConfigureDependenciesService(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services);

//Configurando AutoMapper pára mapear dtos
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new DtoToModelProfile());
    cfg.AddProfile(new EntityToDtoProfile());
    cfg.AddProfile(new ModelToEntityProfile());
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var signingConfigurations = new SigningConfigurations();
builder.Services.AddSingleton(signingConfigurations);

//var tokenConfiguration = new TokenConfiguration();
// new ConfigureFromConfigurationOptions<TokenConfiguration>(
//     Configuration("TokenConfiguration").Configure(tokenConfiguration);
// )

builder.Services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("Audience");
                paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("Issuer");

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

// Ativa o uso do token como forma de autorizar o acesso
// a recursos deste projeto
builder.Services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

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

//Executa as migrations, NA INICIALIZAÇÃO,  quando a variável de ambiente estiver setada como "APLICAR"
if (Environment.GetEnvironmentVariable("MIGRATION")!.ToLower() == "APLICAR".ToLower())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<MyContext>();
    db.Database.Migrate();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
