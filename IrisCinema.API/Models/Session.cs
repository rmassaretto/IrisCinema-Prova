namespace IrisCinema.API.Models
{
    public class Session
    {
        public Session()
        {
        }

        public int Id { get; set; }
        public required DateTime StartTime { get; set; }
        public Room Room{ get; set; } = null!;
        public int RoomId { get; set; }

        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }
}