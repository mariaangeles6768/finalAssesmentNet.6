using MySqlConnector;

namespace sna
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MySqlConnection>(_ => new MySqlConnection("server=localhost;user=jjjjjj;password=123;database=snaDatabase"));
        }
    }
}
