namespace IrisCinema.API.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool Reserved { get; set; } = false;
        public Session Session { get; set; } = null!;
        public int SessionId { get; set; }

        public Seat(int number, int sessionId)
        {
            Number = number;
            SessionId = sessionId;
        }

        public Seat()
        {
        }
    }
}
