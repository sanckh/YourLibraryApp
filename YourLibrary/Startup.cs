using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Services;

namespace YourLibraryAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        // Dependency Injection
        public void ConfigureServices(IServiceCollection service)
        {
            service.AddControllers();

            //configure repositories
            service.AddScoped<IBookRepository, BookRepository>();
            service.AddScoped<IAuthorRepository, AuthorRepository>();
            service.AddScoped<IPublisherRepository, PublisherRepository>();
            service.AddScoped<IBook_AuthorRepository, Book_AuthorRepository>();

            //configure the services
            service.AddScoped<IBookService, BookService>();
            service.AddScoped<IAuthorService, AuthorService>();
            service.AddScoped<IPublisherService, PublisherService>();

            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LibraryClientApp", Version = "v1" });
            });


            //database injection
            service.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("YourLibraryDb"));
            });

            //inject jwt token service

        }

        // Middleware
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "YourLibrary v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //AppDbInitializer.Seed(app);

        }
    }
}
