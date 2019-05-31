using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IMDb_replicaAuthorizationServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
        .AddDeveloperSigningCredential()
        .AddInMemoryApiResources(new List<ApiResource>
        {
            new ApiResource("resource.server.api",
                new [] { ClaimTypes.Name, ClaimTypes.Email})
        })
        .AddInMemoryClients(new List<Client>
        {
            new Client
            {
                ClientId = "angular.client",
                ClientName = "Angular Client",
                ClientSecrets = new [] { new Secret("secret".Sha256())  },
                AllowedScopes = new [] { "resource.server.api" },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedCorsOrigins = new [] { "http://localhost:4200/" }
            }
        })
        .AddTestUsers(new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "user",
                Password = "1234",
                Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Test User"),
                    new Claim(ClaimTypes.Email, "email@mail.com")
                }
            }
        });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseIdentityServer();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
