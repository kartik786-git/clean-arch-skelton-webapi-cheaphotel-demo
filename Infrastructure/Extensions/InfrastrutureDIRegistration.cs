using Application.Interface;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class InfrastrutureDIRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<HotelDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("cleanarchskeletonDb"),
            o => o.EnableRetryOnFailure()));

            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddScoped<IHotelRepository,HotelRepository>();
            services.AddScoped<IRoomRepository,RoomRepository>();
            return services;
        }
    }
}
