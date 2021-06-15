using Hotel.Backend.Infrastructure.Data;
using Hotel.Backend.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Backend.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomizedDataStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection AddCustomizedRepository(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
