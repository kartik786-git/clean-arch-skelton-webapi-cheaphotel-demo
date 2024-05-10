using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
            
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasKey(h => h.Id);
            modelBuilder.Entity<Room>().HasKey(r => r.Id);
            modelBuilder.Entity<Booking>().HasKey(b => b.Id);

            modelBuilder.Entity<Hotel>().HasMany(h => h.Rooms).WithOne(r => r.Hotel);
            modelBuilder.Entity<Room>().HasOne(r => r.Hotel).WithMany(h => h.Rooms);
            modelBuilder.Entity<Booking>().HasOne(b => b.Room).WithMany(r => r.Bookings);
        }
    }
}
