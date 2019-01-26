using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using weGOPAY.weGOPAY.Data;
using weGOPAY.weGOPAY.Services.Settlements;
using weGOPAY.weGOPAY.Services.Users;
using weGOPAY.weGOPAY.Services.Wallets;
using weGOPAY.weGOPAY.Services.WalletTransactions;

namespace weGOPAY
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<weGOPAYDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("weGOPAYDbContextConn")));


            //HttpAccessor
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "weGOPAY API", Version = "v1" });
            });


            //Adding Services

            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IWalletServices, WalletServices>();
            services.AddTransient<IWalletTransactionService, WalletTransactionService>();
            services.AddTransient<ISettlementService, SettlementService>();
          


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
                app.UseHsts();
            }

            app.UseCors(builder => builder.AllowAnyHeader()
                                   .AllowAnyMethod()
                                   .AllowAnyOrigin()
                                   .AllowCredentials());
            app.UseHttpsRedirection();

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "weGOPAY API V1");
            });
            app.UseMvc();
        }
    }
}
