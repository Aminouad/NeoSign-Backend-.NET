using Microsoft.EntityFrameworkCore;
using NEOsign.Model;

namespace NEOsign.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
       .HasMany(c => c.Documents)
       .WithOne(u => u.User)
       .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
      .HasMany(c => c.Company)
      .WithOne(u => u.User)
      .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
      .HasMany(c => c.Personnels)
      .WithOne(u => u.User)
      .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Document>()
     .HasOne(c => c.User)
     .WithMany(u => u.Documents)
     .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Personnel>()
     .HasOne(c => c.User)
     .WithMany(u => u.Personnels).HasForeignKey(p=> p.UserId)
     .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
