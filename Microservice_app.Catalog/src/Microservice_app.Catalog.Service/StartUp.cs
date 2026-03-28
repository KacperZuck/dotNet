
using Microservice_app.Catalog.Service.Model;

namespace Microservice_app.Catalog.Service
{
    public class StartUp
    {
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration {get;}


        public void ConfigureServices(IServiceCollection services)
        {
            var serviceSettings = Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

            services.AddMongo().AddMongoRepo<Item>("items");

            services.AddControllers( options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
        }
    }
}