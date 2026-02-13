using IrisCinema.API.DTO.Seat;

namespace IrisCinema.API.DTO.Session
{
    public class SessionDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int RoomId { get; set; }
        public List<SeatDto> Seats { get; set; } = new();
    }
}
