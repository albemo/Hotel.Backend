using Hotel.Backend.Infrastructure.Repository;
using Hotel.Backend.WebApi.Interfaces;

namespace Hotel.Backend.WebApi.Services
{
    public class HotelService : BaseRepository<Domain.Models.Hotel>, IHotelService
    {
        public HotelService(IRepository<Domain.Models.Hotel> repository): base(repository)
        {
        }
    }
}
