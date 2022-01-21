using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Configuration;
using Nancy.Owin;
using Nancy.TinyIoc;
using System;

namespace nancy_netcoreapp_sample
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseOwin(pipeline => pipeline.UseNancy(options =>
            {
                options.Bootstrapper = new Bootstrapper(app.ApplicationServices);
            }));
        }
    }

    public class Bootstrapper: DefaultNancyBootstrapper
    {
        readonly IServiceProvider _serviceProvider;

        public Bootstrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(true, true);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register(_serviceProvider.GetService<ILoggerFactory>());
        }
    }
}
