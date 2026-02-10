namespace IrisCinema.API.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool Reserved { get; set; }
        public required Session Session { get; set; }
    }
}
