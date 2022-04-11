using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trace.BLL;
using Trace.Repository;
using Utils;

namespace Trace.Api
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
            services.AddCors(option => {
                option.AddPolicy("CrosPolicy", policy =>
                {
                    //註冊CORS的Policy規則
                    //設定允許的跨域來源、允許任何的 Request Header、允許任何的 HTTP Method、允許憑證(CORS 的憑證，如：Cookies)
                    policy.WithOrigins("http://localhost:8080", "http://predictive-fx-316103.an.r.appspot.com")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Trace", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

            });
            services.AddDbContext<ApplicationDbContext>(
                     options =>
                     options.UseSqlServer(Configuration.GetConnectionString("ApplicationDb")));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserFriendRepository, UserFriendRepository>();
            services.AddScoped<IUserRecordRepository, UserRecordRepository>();
            services.AddScoped<ITripEventRepository, TripEventRepository>();
            services.AddScoped<ITripGroupRepository, TripGroupRepository>();
            services.AddScoped<ITripArticleRepository, TripArticleRepository>();
            services.AddScoped<ITripPhotoRepository, TripPhotoRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();

            services.AddScoped<IUserCenter, UserCenter>();
            services.AddScoped<ITripCenter, TripCenter>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CrosPolicy");
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(ConfigurationHelper.GetSection("TripPhotoDir").Value),
                RequestPath = new PathString("/photo")
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trace v1"));
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
