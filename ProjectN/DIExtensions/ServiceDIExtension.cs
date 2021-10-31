using Microsoft.Extensions.DependencyInjection;
using ProjectN.Service.Implement;
using ProjectN.Service.Interface;

namespace ProjectN.DIExtensions
{
    /// <summary>
    /// Service 相關註冊
    /// </summary>
    public static class ServiceDIExtension
    {
        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICardService, CardService>();

            return services;
        }
    }
}
