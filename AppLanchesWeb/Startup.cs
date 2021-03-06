using AppLanchesWeb.Context;
using AppLanchesWeb.Repository;
using AppLanchesWeb.Repository.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLanchesWeb
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
            //adiciona a implementa��o padr�o do IDistributedCache [Sess�o]
            services.AddDistributedMemoryCache();
            //adicionando  a se��o [Sess�o]
            services.AddSession();
            //Registrando as Interfaces para inje��o de dependencias [Inje��o de Dependencias]
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ILancheRepository, LancheRepository>();

           // configurando conex�o com o Bajco de dados  que acessa o Metodo Json  que vai acessar a Default que defin[Conex�o]  
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //usando a Sess�o [Sess�o]
            app.UseSession();

            //verifica se estou em anbiente de desenvolvimento se sim me mostra uma exe��o mais detalahada
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //define que pode acessar os arquivos estaticos da pasta wwwroot,css,js e entre outros
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization(); 

            //defina a rota inicial que o Projeto vai tomar
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
