using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class ApplicationDIRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        { 
            // add our depdency

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(m => m.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
        }
    }
}
