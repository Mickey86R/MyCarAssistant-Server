using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using APi_MyCar_POS.Models;

namespace APi_MyCar_POS
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string con = "connectionstring";
            
            // устанавливаем контекст данных
            services.AddDbContext<MyContext>(options => options.UseSqlServer(con));

            services.AddControllers(); // используем контроллеры без представлений
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
            });
        }
    }
}