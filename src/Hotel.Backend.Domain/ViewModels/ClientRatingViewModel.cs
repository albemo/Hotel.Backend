namespace Hotel.Backend.Domain.ViewModels
{
    public class ClientRatingViewModel
    {
        public int Id { get; set; }

        public int Score { get; set; }

        public string Comment { get; set; }

        public int ClientId { get; set; }

        public ClientViewModel Client { get; set; }

        public int HotelId { get; set; }

        public HotelViewModel Hotel { get; set; }
    }
}
