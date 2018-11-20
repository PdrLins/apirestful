using DesafioPitang.Business.Interfaces;
using DesafioPitang.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text;
using DesafioPitang.Business.Services;

namespace DesafioPitang
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
            services.AddMvc();
            services.AddCors();

            var tokenSettings = new TokenSettings();
            new ConfigureFromConfigurationOptions<TokenSettings>(Configuration.GetSection("TokenConfig")).Configure(tokenSettings);


            services.AddSingleton(tokenSettings);
            services.Configure<IISOptions>(options => {
                options.ForwardClientCertificate = false;
            });
            services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase("InMemoryDatabase"));

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(beare =>
            {
                beare.RequireHttpsMetadata = false;
                beare.SaveToken = true;
                beare.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.Key)),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(cors =>
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default_route", "api/{controller}/{action}/{id?}", new { controller = "Values", action = "Get" });
            });
        }
    }
}
