using Microsoft.Extensions.DependencyInjection;
using ProjectN.Repository.Implement;
using ProjectN.Repository.Interface;

namespace ProjectN.DIExtensions
{
    /// <summary>
    /// Repo 相關 DI 註冊
    /// </summary>
    public static class RepositoryDIExtension
    {
        /// <summary>
        /// Registers the repos.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICardRepository>(sp =>
            {
                var connectString = @"Server=(LocalDB)\MSSQLLocalDB;Database=Newbie;Trusted_Connection=True;";
                return new CardRepository(connectString);
            });

            return services;
        }
    }
}
