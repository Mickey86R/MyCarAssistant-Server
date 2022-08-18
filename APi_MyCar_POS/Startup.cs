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
            
            // ������������� �������� ������
            services.AddDbContext<MyContext>(options => options.UseSqlServer(con));

            services.AddControllers(); // ���������� ����������� ��� �������������
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // ���������� ������������� �� �����������
            });
        }
    }
}