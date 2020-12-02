using Autofac;
using AutoMapper;
using GolovinskyAPI.Data;
using GolovinskyAPI.Logic.Profiles;
using GolovinskyAPI.Logic.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace GolovinskyAPI.Web
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
            services.AddCors();
            
            Global.Connection = Configuration.GetConnectionString("DefaultConnection");

            //services.AddTransient<IAuthHandler, AuthHandler>();
            services.AddAutoMapper(typeof(AdminProfile), typeof(CatalogProfile), typeof(PictureProfile));
            services.AddOptions();
            services.AddMvc();
            services.AddControllers().AddNewtonsoftJson();  

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
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();  // added in ver.2.1
            app.UseDefaultFiles();
            app.UseStaticFiles();            
            app.UseRouting();

            app.UseCors(options => options
                .AllowAnyOrigin()
                //.WithOrigins(Configuration.GetSection("CorsOrigins").Get<string[]>())
                .AllowAnyMethod()
                .AllowAnyHeader()
                //.AllowCredentials()
                );

            app.UseAuthentication();
            app.UseSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

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