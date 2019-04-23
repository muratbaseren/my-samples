using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using WebApiToken.Services;

namespace WebApiToken
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
            //byte[] key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("AppSettings:AppKey"));

            services.AddDbContext<DatabaseContext>(opt => opt.UseInMemoryDatabase("webapitokensampledb"));

            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddCors();
            services.AddAutoMapper();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAppsService, AppsService>();

            services.Configure<AppsSetting>(Configuration.GetSection("Apps"));

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            //{
            //    //opt.SaveToken = true;
            //    //opt.RequireHttpsMetadata = false;
            //    opt.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuerSigningKey = true
            //    };
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(pb => pb
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("http://localhost:65286/", "domain2"));

            //app.UseAuthentication();

            app.UseMvc();

        }
    }
}
