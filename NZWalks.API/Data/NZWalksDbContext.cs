using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Difficulties

            var difficulties = new List<Difficulty>
            {
                new Difficulty
                {
                    Id = Guid.Parse("e6c626ae-c6db-4787-a374-2b4eeb36a234"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("c7b18847-5268-444b-9283-d8fd47bf2486"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("fcf83289-488c-4d2c-8e04-9d63755f5d8d"),
                    Name = "Hard"
                }
            };

            // Seed difficulties into the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            // Seed data for Regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("8bd0927d-bf7e-4cfc-800b-0ab8260b97b1"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageURL = "https://images.pexels.com/photos/29724794/pexels-photo-29724794.jpeg"
                },

                new Region
                {
                    Id = Guid.Parse("880eba07-6f2d-4e6c-baff-a42255df86c8"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageURL = "https://images.pexels.com/photos/12348118/pexels-photo-12348118.jpeg"
                },

                new Region
                {
                    Id = Guid.Parse("3e0f0ab4-19b2-46a6-b074-c48a359d4654"),
                    Name = "Bay of Plenty",
                    Code = "BOP",
                    RegionImageURL = null
                },

                new Region
                {
                    Id = Guid.Parse("ce4b2919-a415-48e4-8f4e-99e332f863d9"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageURL = "https://images.pexels.com/photos/32540627/pexels-photo-32540627.jpeg"
                },

                new Region
                {
                    Id = Guid.Parse("254749c3-5f7f-488e-a330-8025f483238c"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageURL = null
                },

                new Region
                {
                    Id = Guid.Parse("2f05301e-009a-403d-827a-68c5f4c63c32"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageURL = "https://images.pexels.com/photos/1353248/pexels-photo-1353248.jpeg"
                }
            };

            // Seed regions into the database
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
