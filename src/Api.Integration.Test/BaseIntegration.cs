//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Api.CrossCutting.DtoMappings;
using Api.Data.Context;
using Api.Domain.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting.Server;

namespace Api.Integration.Test
{
    public class BaseIntegration
    {
        public MyContext? myContext { get; private set; }
        public HttpClient client { get; private set; }
        public IMapper mapper { get; set; }
        public string hostApi { get; set; }

        //public HttpResponseMessage? response { get; set; }  

        public BaseIntegration()
        {
            hostApi = "http://localhost:5175/api";

            // Configure and create HttpClient mock
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddSingleton<IServer, TestServer>();
            using var application = builder.Build();

            // var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            // {
            //     builder.ConfigureServices(services =>
            //     {
            //         // set up servises
            //         services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MyContext>));

            //     });
            //     builder.UseEnvironment("Testing");
            //     //builder.UseTestServer();
            // });

            // //server = new application.

            // //myContext = application.Server.Host.Services.GetService(typeof(MyContext)) as MyContext;
            // .myContext!.Database.Migrate();

            mapper = new AutoMapperFixture().GetMapper();

            //client = application.Server.CreateClient();
        }

        public async Task AdicionarToken()
        {
            var loginDto = new LoginDto()
            {
                Email = "mm@gmail.com"
            };

            var resultLogin = await PostJsonAsync(loginDto, $"{hostApi}login", client);
            var jsonLogin = await resultLogin.Content.ReadAsStringAsync();
            var loginObject = JsonConvert.DeserializeObject<LoginResponseDto>(jsonLogin);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                                                         loginObject!.accessToken);

        }

        public static async Task<HttpResponseMessage> PostJsonAsync(object dataclass, string url, HttpClient client)
        {
            return await client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(dataclass), System.Text.Encoding.UTF8, "application/json"));
        }

        public void Dispose()
        {
            myContext!.Dispose();
            client.Dispose();
        }
    }
    public class AutoMapperFixture : IDisposable
    {
        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelToEntityProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });
            return config.CreateMapper();
        }
        public void Dispose() { }
    }

}
