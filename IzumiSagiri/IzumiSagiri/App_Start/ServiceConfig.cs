using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IzumiSagiri.App_Start
{
    public class ServiceConfig
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
            app.UseWelcomePage();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // Add all dependencies needed by Mvc.
            services.AddMvc();

            // Add TodoRepository service to the collection. When an instance of the repository is needed,
            // the framework injects this instance to the objects that needs it (e.g. into the TodoController).
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.Populate(services);

            IContainer container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }
    }
}