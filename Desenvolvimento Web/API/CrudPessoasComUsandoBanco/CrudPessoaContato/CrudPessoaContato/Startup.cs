using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudPessoaContato
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
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(name: "Mypolicy",
                                  policy =>
                                  {
                                      policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                                  });
            });
            //services.AddControllers(options =>
            //{
            //    options.AllowEmptyInputInBodyModelBinding = true;
            //    foreach (var formatter in options.InputFormatters)
            //    {
            //        if (formatter.GetType() == typeof(SystemTextJsonInputFormatter))
            //            ((SystemTextJsonInputFormatter)formatter).SupportedMediaTypes.Add(
            //            Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/plain"));
            //    }
            //}).AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors("Mypolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
