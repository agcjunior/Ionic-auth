using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using authApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace authApi
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
            var settings = GetJwtSettings();

            services.AddSingleton<JwtSettings>(settings);

            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", jwtOptions => {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(settings.Key)),
                    
                    ValidateIssuer = true,
                    ValidIssuer = settings.Issuer,
                    
                    ValidateAudience = true,
                    ValidAudience = settings.Audience,
                    
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(settings.MinutosToExpiration)
                };
            } );

            services.AddAuthorization();

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddDbContext<AuthDbContext>(options => 
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            
            services.AddScoped<SecurityManager>();
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

            
            app.UseCors(builder => 
                builder.WithOrigins("http://localhost:8100")
                        .AllowAnyHeader()
                        .AllowAnyMethod());

            app.UseAuthentication();
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public JwtSettings GetJwtSettings(){
            var JwtSettings = new JwtSettings{
                Key = Configuration["JwtSettings:key"],
                Issuer = Configuration["JwtSettings:issuer"],
                Audience = Configuration["JwtSettings:audience"],
                MinutosToExpiration = Convert.ToInt32(
                    Configuration["JwtSettings:minutosToExpiration"]
                )
            };

            return JwtSettings;
        }
    }
}
