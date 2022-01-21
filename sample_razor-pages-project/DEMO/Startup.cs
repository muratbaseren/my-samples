using DEMO.Entities;
using MFramework.Services.FakeData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO
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
            services.AddDistributedMemoryCache();
            services.AddSession(opts =>
            {
                opts.Cookie.Name = "expenseapp.session";
                opts.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            services.AddRazorPages();

            AddInitialData();
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
            }
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private static void AddInitialData()
        {
            DatabaseContext db = new DatabaseContext();

            if (db.Users.Any() == false)
            {
                db.Users.Add(new User { Username = "muratbaseren", Password = "123123" });
                db.SaveChanges();
            }

            if (db.Expenses.Any() == false)
            {
                string[] categories = new string[] { "Market", "Elektronik", "Tatil", "Taký" };
                int userId = db.Users.FirstOrDefault().Id;

                for (int i = 0; i < 50; i++)
                {
                    db.Expenses.Add(new Expense
                    {
                        Category = CollectionData.GetElement(categories),
                        Date = DateTimeData.GetDatetime(),
                        Description = TextData.GetSentence(),
                        Price = NumberData.GetNumber(100, 500),
                        UserId = userId
                    });
                }

                db.SaveChanges();
            }
        }
    }
}
