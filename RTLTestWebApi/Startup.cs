using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;
using RTLTest.Services;
using RTLTest.Services.AutoMapper;
using RTLTestTask.Db;

namespace RTLTestWebApi
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
            services.AddDbContext<RTLDbContext>(opt => opt.UseInMemoryDatabase("RTLMemoryDb"));
            //services.AddDbContext<RTLDbContext>(opt => opt.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=rtl;Integrated Security=true;"));

            services.AddSingleton<ModelMapper>();
            services.AddTransient<IDataService, DataService>();
            services.AddTransient<IScrapperService, ScrapperService>();

            services.AddHostedService<DataSyncService>();

            services.AddRefitClient<ITVMazeClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://api.tvmaze.com"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
