using System.Collections.Generic;

namespace Hotel.Backend.Domain.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<ClientRating> ClientRatings { get; set; } = new List<ClientRating>();
    }
}
