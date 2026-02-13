namespace IrisCinema.API.DTO.Reservation
{
    public class ReservationResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SeatId { get; set; }
    }
}
