using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prime.Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Prime.Backend
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
            //var connection = Configuration["Production:SqliteConnectionString"];
            var connection = "Data Source=C:\\Temp\\data.sqlite";

        //services.AddDbContext<MessageRecordContext>(options => 
        //    options.UseInMemoryDatabase("PRIMEDB")
        //);
        services.AddDbContext<MessageRecordContext>(options =>
                options.UseSqlite(connection)
            );
            services.AddMvc(o => 
                o.InputFormatters.Insert(0, new BinaryMessageFormatter())
            );
            //services.AddScoped<IDataEventRecordRepository, DataEventRecordRepository>();
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
