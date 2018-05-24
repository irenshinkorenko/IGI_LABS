using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Application.Models;
using Lab5.Data;

namespace Application
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
            //Sqlite
            services.AddDbContext<HospitalContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));
            //SQL Server
            //services.AddDbContext<CouncilDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("CouncilConnectionSQL")));
            //MySQL
            //services.AddDbContext<CouncilDbContext>(options =>
            //    options.UseMySQL(Configuration.GetConnectionString("CouncilConnectionMysql")));

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, HospitalContext context)
        {

            app.UseStaticFiles();
            app.UseMvc();
            // инициализация базы данных
            DbInitializer.Initialize(context);
        }
    }
}
