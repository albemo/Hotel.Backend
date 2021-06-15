using Hotel.Backend.Domain.Models;
using Hotel.Backend.Infrastructure.Repository;
using Hotel.Backend.WebApi.Interfaces;

namespace Hotel.Backend.WebApi.Services
{
    public class ClientRatingService : BaseRepository<ClientRating>, IClientRatingService
    {
        public ClientRatingService(IRepository<ClientRating> repository) : base(repository)
        {
            // métodos transversales
        }
    }
}
