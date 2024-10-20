namespace HouseHero.Models
{
    public class ConfigureServices
    {
        public IConfiguration Configuration { get; }

        public ConfigureServices(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void RegisterServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            
        }
    }
}
