using System.Collections.Generic;

namespace Hotel.Backend.Domain.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<ClientRatingViewModel> ClientRatings { get; set; } = new List<ClientRatingViewModel>();
    }
}
