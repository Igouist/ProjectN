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
    /// �Ұ��I
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// �Ұ��I
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// �պA�]�w
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ���U�A��
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

            // API �A��²��
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "���� API",
                    Description = "�����s�V�O���d�� API",
                    TermsOfService = new Uri("https://igouist.github.io/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Igouist",
                        Email = string.Empty,
                        Url = new Uri("https://igouist.github.io/about/"),
                    }
                });

                // Ū�� XML �ɮײ��� API ����
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// �պA�]�w
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
