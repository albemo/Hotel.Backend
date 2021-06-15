using Hotel.Backend.Domain.Models;
using Hotel.Backend.Infrastructure.Repository;
using Hotel.Backend.WebApi.Interfaces;

namespace Hotel.Backend.WebApi.Services
{
    public class ClientService : BaseRepository<Client>, IClientService
    {
        public ClientService(IRepository<Client> repository) : base(repository)
        {
            // métodos transversales
        }
    }
}
