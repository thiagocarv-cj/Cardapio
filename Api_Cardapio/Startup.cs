using Infraestrutura;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api_Cardapio
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuração do Entity Framework Core
            services.AddDbContext<AplicacaoDbContext>(options =>
                options.UseSqlServer("Server=DESKTOP-PDHCE1Q;Database=Cardapio;Integrated Security=true;TrustServerCertificate=true"));

            // Configuração do Mediator
            services.AddMediatR(typeof(Startup));

            // Outros serviços
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
