namespace IrisCinema.API.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public required Seat Seat { get; set; }
    }
}
