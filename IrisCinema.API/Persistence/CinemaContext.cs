using IrisCinema.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IrisCinema.API.Persistence
{
    public class CinemaContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public string DbPath { get; }

        public CinemaContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "cinema.db");
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
