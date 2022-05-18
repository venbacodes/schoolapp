using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public const string Roles = "roles";
        public const string Role = "role";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _config = configuration;
            _webHostEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddOpenIdConnect(options =>
                {
                    SetOpenIdConnectionOptions(options, _webHostEnvironment);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private static void SetOpenIdConnectionOptions(OpenIdConnectOptions options, IWebHostEnvironment env)
        {
            options.Authority = env.IsDevelopment() ? "https://localhost:44302" : "https://localhost:44302";
            options.ClientId = "school_app_client";
            options.ClientSecret = "school_app_secret";

            options.Scope.Add("profile");
            options.Scope.Add("openid");
            options.ResponseType = "code";
            options.SaveTokens = true;
            options.GetClaimsFromUserInfoEndpoint = true;
            options.ClaimActions.MapUniqueJsonKey(Role, Role);
            options.TokenValidationParameters = new TokenValidationParameters
            {
                RoleClaimType = Role
            };
        }
    }
}
