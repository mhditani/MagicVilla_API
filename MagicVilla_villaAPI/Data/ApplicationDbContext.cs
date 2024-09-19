using MagicVilla_villaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_villaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "Royal Villa provides beachfront accommodations in Trabzon. This villa offers free private parking and a shared kitchen.",
                    ImageUrl = "https://limestays.com/wp-content/uploads/2023/01/34-926x618.jpg",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 550,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 2,
                    Name = "Premium Pool Villa",
                    Details = "Explore your family getaway with our premium pool villa, featuring a spacious room with two king-sized beds and a private pool right in your room.",
                    ImageUrl = "https://image-tc.galaxy.tf/wijpeg-vh7o2qzd9xv299ah5v1vav87/grand-lexis-port-dickson-premium-pool-villa-3-thumbnail-w4896_wide.jpg?crop=0%2C105%2C2000%2C1125&width=1140",
                    Occupancy = 4,
                    Rate = 300,
                    Sqft = 550,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 3,
                    Name = "Luxury Pool Villa",
                    Details = "Featuring a garden, Highend Private Pool Villa features accommodations in Amphoe Koh Samui. This beachfront property offers access to a balcony.",
                    ImageUrl = "https://www.luva-villas.com/img/news_11_crop_0_1671810979.webp",
                    Occupancy = 4,
                    Rate = 400,
                    Sqft = 750,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 4,
                    Name = "Diamond Villa",
                    Details = "Diamond Villa has view of islands and the Aegean Sea, with private pool. The villa offers accommodation with butler service, maximized luxury and advantages.",
                    ImageUrl = "https://static.wixstatic.com/media/b32baf_ff2d9aeb3aff4cb99694364312052495~mv2.jpg/v1/fill/w_640,h_426,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/b32baf_ff2d9aeb3aff4cb99694364312052495~mv2.jpg",
                    Occupancy = 4,
                    Rate = 550,
                    Sqft = 900,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 5,
                    Name = "Diamond Pool Villa",
                    Details = "Providing an outdoor swimming pool, Diamond Pool Villa@Samui provides accommodations in Koh Samui. This chalet features free private parking, free shuttle.",
                    ImageUrl = "https://cdn-5d68e683f911c80950255463.closte.com/wp-content/uploads/2019/05/Edit_Overview_Villa-2.jpg",
                    Occupancy = 4,
                    Rate = 600,
                    Sqft = 1100,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                }
            );
        }
    }
}
