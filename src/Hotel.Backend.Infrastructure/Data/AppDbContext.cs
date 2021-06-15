using Hotel.Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Backend.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<ClientRating> ClientRatings { get; set; }

        public DbSet<Domain.Models.Hotel> Hotels { get; set; }

        public DbSet<Image> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientRating>()
                .HasOne<Client>(x => x.Client)
                .WithMany(x => x.ClientRatings)
                .HasForeignKey(x => x.ClientId);

            modelBuilder.Entity<ClientRating>()
                .HasOne<Domain.Models.Hotel> (x => x.Hotel)
                .WithMany(x => x.ClientRatings)
                .HasForeignKey(x => x.HotelId);

            modelBuilder.Entity<Image>()
                .HasOne<Domain.Models.Hotel>(x => x.Hotel)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.HotelId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
