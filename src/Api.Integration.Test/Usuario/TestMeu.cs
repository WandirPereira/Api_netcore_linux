using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Integration.Test.Usuario
{

    public class TestMeu
    {
        [Fact]
        public async Task Test1()
        {
            // Configure and create HttpClient mock
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddSingleton<IServer, TestServer>();
            await using var application = builder.Build();

            application.MapGet("/", () => "Hello meziantou").RequireHost("meziantou.net");
            application.MapGet("/", () => "Hello contoso").RequireHost("contoso.com");
            application.MapGet("/", () => "Hello localhost");
            application.MapGet("/{id}", (int id) => Results.Ok(new { id = id, name = "Sample" }));

            _ = application.RunAsync();
            using var httpClient = ((TestServer)application.Services.GetRequiredService<IServer>()).CreateClient();

            // Use the HttpClient mock
            Assert.Equal("Hello localhost", await httpClient.GetStringAsync("/"));
            Assert.Equal("""{"id":10,"name":"Sample"}""", await httpClient.GetStringAsync("/10"));
            //Assert.Equal("Hello meziantou", await httpClient.GetStringAsync("https://www.meziantou.net/"));
            //Assert.Equal("Hello contoso", await httpClient.GetStringAsync("http://contoso.com/"));
        }
    }
}
