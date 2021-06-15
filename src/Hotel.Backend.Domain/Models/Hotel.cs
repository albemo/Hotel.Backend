using Hotel.Backend.Domain.Enums;
using System.Collections.Generic;

namespace Hotel.Backend.Domain.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }

        public double Price { get; set; }

        public IList<Image> Images { get; set; } = new List<Image>();

        public IList<ClientRating> ClientRatings { get; set; } = new List<ClientRating>();
    }
}
