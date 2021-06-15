using Hotel.Backend.Domain.Models;
using Hotel.Backend.Infrastructure.Repository;
using Hotel.Backend.WebApi.Interfaces;

namespace Hotel.Backend.WebApi.Services
{
    public class ImageService : BaseRepository<Image>, IImageService
    {
        public ImageService(IRepository<Image> repository) : base(repository)
        {
        }
    }
}
