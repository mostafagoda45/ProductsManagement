using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsManagement.Application.Interfaces;
using ProductsManagement.Domain.Settings;
using ProductsManagement.Infrastructure.Shared.Services;

namespace ProductsManagement.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
