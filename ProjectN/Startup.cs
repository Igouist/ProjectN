using System;
using System.IO;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProjectN.Parameter;
using ProjectN.Repository.Implement;
using ProjectN.Repository.Interface;
using ProjectN.Service.Implement;
using ProjectN.Service.Interface;
using ProjectN.Validators;

namespace ProjectN
{
    /// <summary>
    /// 啟動點
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 啟動點
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// 組態設定
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 註冊服務
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICardRepository>(sp =>
            {
                var connectionString = this.Configuration.GetValue<string>("ConnectionString");
                return new CardRepository(connectionString);
            });

            services.AddScoped<ICardService, CardService>();

            services.AddControllers();

            //services.AddFluentValidation();
            //services.AddTransient<IValidator<CardParameter>, CardParameterValidator>();

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // API 服務簡介
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "菜雞 API",
                    Description = "菜雞新訓記的範例 API",
                    TermsOfService = new Uri("https://igouist.github.io/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Igouist",
                        Email = string.Empty,
                        Url = new Uri("https://igouist.github.io/about/"),
                    }
                });

                // 讀取 XML 檔案產生 API 說明
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// 組態設定
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
