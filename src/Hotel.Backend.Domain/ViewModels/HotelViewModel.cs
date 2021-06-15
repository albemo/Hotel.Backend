using Hotel.Backend.Domain.Enums;
using System.Collections.Generic;

namespace Hotel.Backend.Domain.ViewModels
{
    public class HotelViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }

        public double Price { get; set; }

        public IList<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();

        public IList<ClientRatingViewModel> ClientRatings { get; set; } = new List<ClientRatingViewModel>();
    }
}
