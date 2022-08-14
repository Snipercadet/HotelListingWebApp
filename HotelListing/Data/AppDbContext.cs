using HotelListing.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id=1,
                    Name = "Nigeria",
                    ShortName = "NG"
                },
                new Country
                {
                    Id =2,
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                new Country
                {
                    Id = 3,
                    Name = "Bahamas",
                    ShortName = "BS"
                });

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                     Id=1,
                     Name="Epitome Hotel and Suites",
                     Address = "Barnawa",
                     Rating = 4.5,
                     CountryId = 1
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Sandals Resort and Spa",
                    Address = "Negril",
                    Rating = 3,
                    CountryId = 2
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Grand Palladium",
                    Address = "Nassau",
                    Rating = 5,
                    CountryId = 3
                });
        }
    }
}
