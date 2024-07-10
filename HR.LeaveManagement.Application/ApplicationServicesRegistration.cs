using HR.LeaveManagement.Application.Profiles;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HR.LeaveManagement.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(c => c.AddProfile(typeof(MappingProfile)));

            services.AddMediatR(Assembly.GetExecutingAssembly());

            //services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
