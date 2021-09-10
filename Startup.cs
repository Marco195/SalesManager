using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesManager.Data;

namespace SalesManager
{

    /// <summary>
    /// Configura a inicialização da aplicação, incluindo rotas e qual DB.
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configura os serviços a serem utilizados pela aplicação.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Método Antigo, que utiliza o SQL Server.
            //services.AddDbContext<SalesManagerContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("SalesManagerContext")));

            //Adicionar ao projeto o EntityFramework na versão 2.1.1 que é a mesma do .Net Core.
            //Devo utilizar o nome da classe criada na pasta "Data".
            services.AddDbContext<SalesManagerContext>(options =>
                                                        options.UseMySql(Configuration.GetConnectionString("SalesManagerContext"), builder =>
                                                        builder.MigrationsAssembly("SalesManager")));//Deixar o nome da solução
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //Em caso de erro, ele irá direcionar para a seguinte página abaixo.
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //Aponta a rota padrão que a aplicação irá seguir.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
