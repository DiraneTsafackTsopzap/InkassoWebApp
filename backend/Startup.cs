using backend.dataContext;
using backend.Repository.ForderungsRepositories;
using backend.Repository.KundeRepositories;
using backend.Repository.SchuldnerRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            //1- Hinzufugung des Controllers
            services.AddControllers()
                    .AddJsonOptions(options =>
                     {
                         options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                     });



            // 2- Hinzufugung des ConnectionsStrings
            services.AddDbContext<BackendContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));


            // 3- H. des Repositorys
            services.AddScoped<IForderungsRepository, ForderungsRepository>();
            services.AddScoped<IKundeRepository, KundeRepository>();
            services.AddScoped<ISchuldnerRepository, SchuldnerRepository>();


            // 4- H. des  JwtBearer Token

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["MeinKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            // 5- H. des Cors
            // Cors
            services.AddCors(options => options.AddPolicy("AlloWebApp", builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AlloWebApp");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
