using GolovinskyAPI.Infrastructure;
using GolovinskyAPI.Infrastructure.Administration;
using GolovinskyAPI.Infrastructure.Handlers;
using GolovinskyAPI.Infrastructure.Interfaces;
using GolovinskyAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Linq;
using System.Text;

namespace GolovinskyAPI
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
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("https://xn--b1abqafeesfb0b.xn--p1ai").AllowAnyMethod().AllowAnyHeader();
                builder.WithOrigins("https://xn--e1arkeckp8bt.xn--p1ai").AllowAnyMethod().AllowAnyHeader();
                builder.WithOrigins("http://xn--e1arkeckp8bt.xn--p1ai").AllowAnyMethod().AllowAnyHeader();
            }));

            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddTransient<IRepository, Repository>(provider => new Repository(connection));
            services.AddTransient<IProductRepository, ProductRepository>(provider => new ProductRepository(connection));
            services.AddTransient<ITemplateRepository, TemplateRepository>(provider => new TemplateRepository(connection));
            services.AddTransient<ICatalogRepository, CatalogRepository>(provider => new CatalogRepository(connection));
            services.AddTransient<IAuthHandler, AuthHandler>();
            // services.AddTransient<ISms_aero, Sms_aero>();
            services.AddOptions();
            services.AddMvc();
            
            services.Configure<AuthServiceModel>(Configuration.GetSection("AuthService"));
            var result = Configuration.GetSection("AuthService").GetChildren();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo { Title = "Test Api", Description = "Swagger Test Api" });
                var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"GolovinskyAPI.xml";
                c.IncludeXmlComments(xmlPath);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata=false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = result.Where(x => x.Key == "Issuer").FirstOrDefault().Value,
                        ValidateAudience = false,
                        ValidAudience = result.Where(x => x.Key == "Audience").FirstOrDefault().Value,
                        ValidateLifetime = true,
                        IssuerSigningKey = GetSymmetricSecurityKey(result.Where(x => x.Key == "Key").FirstOrDefault().Value),
                        ValidateIssuerSigningKey = true 
                    }; 
                });
            services.AddControllers(); // added in ver.3.0
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // added in ver.2.1
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();  // added in ver.2.1
            app.UseDefaultFiles();
            app.UseStaticFiles();            
            app.UseRouting(); // added in ver.3.0

            //var origins = Configuration.GetSection("CorsOrigins").GetChildren().ToArray().Select(c => c.Value).ToArray();
            app.UseCors("ApiCorsPolicy");
            //app.UseCors(builder => builder.AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .AllowCredentials()
            //    );
            
            // app.UseDeveloperExceptionPage();
            app.UseAuthentication();
            // app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "Test Api");
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot","Images")),                    
                RequestPath = "/mainimages"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "AccountImages")),
                RequestPath = "/accountImages"
            });
        }
    }
}