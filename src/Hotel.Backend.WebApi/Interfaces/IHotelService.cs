using Hotel.Backend.Infrastructure.Repository;

namespace Hotel.Backend.WebApi.Interfaces
{
    public interface IHotelService : IRepository<Domain.Models.Hotel>
    {
    }
}
