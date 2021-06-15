namespace Hotel.Backend.Domain.Models
{
    public class ClientRating
    {
        public int Id { get; set; }

        public int Score { get; set; }

        public string Comment { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }
    }
}
