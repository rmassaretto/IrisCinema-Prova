namespace IrisCinema.API.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public required DateTime StartTime { get; set; }
        public required Room Room { get; set; }
        public bool Status { get; set; }
    }
}