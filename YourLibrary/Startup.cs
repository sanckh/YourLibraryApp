using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
            service.AddControllers().AddNewtonsoftJson();

            //Add the Context Accessor
            service.AddHttpContextAccessor();

            //configure repositories
            service.AddScoped<IBookRepository, BookRepository>();
            service.AddScoped<IAuthorRepository, AuthorRepository>();
            service.AddScoped<IPublisherRepository, PublisherRepository>();
            service.AddScoped<IBook_AuthorRepository, Book_AuthorRepository>();
            service.AddScoped<IUserRepository, UserRepository>();


            //configure the services
            service.AddScoped<IBookService, BookService>();
            service.AddScoped<IAuthorService, AuthorService>();
            service.AddScoped<IPublisherService, PublisherService>();
            service.AddScoped<IAccountService, AccountService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IGoogleBooksService, GoogleBooksService>();

            //API Service(s)
            service.AddHttpClient<GoogleBooksService>();

            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LibraryClientApp", Version = "v1" });
            });


            //database injection
            service.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("YourLibraryDB"));
            });

            //inject jwt token service
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Issuer"],
                    ValidAudience = Configuration["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["PrivateKey"]))
                };
            });

            //adding CORS Middleware
            service.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });


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
            app.UseCors("AllowAnyOrigin");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            //AppDbInitializer.Seed(app);

        }
    }
}
