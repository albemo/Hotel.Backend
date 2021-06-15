using Hotel.Backend.WebApi.Interfaces;
using Hotel.Backend.WebApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Backend.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomizedServices(this IServiceCollection services)
        {
            services.AddTransient<IClientRatingService, ClientRatingService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IHotelService, HotelService>();
            services.AddTransient<IImageService, ImageService>();

            return services;
        }
    }
}
