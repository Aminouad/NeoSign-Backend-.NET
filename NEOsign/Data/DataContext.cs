using Microsoft.EntityFrameworkCore;
using NEOsign.Model;

namespace NEOsign.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Certificate> Certificates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
       .HasMany(c => c.Documents)
       .WithOne(u => u.User)
       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
      .HasMany(c => c.Companies)
      .WithOne(u => u.User)
      .OnDelete(DeleteBehavior.Cascade);
       
            
            modelBuilder.Entity<Document>()
     .HasOne(c => c.User)
     .WithMany(u => u.Documents)
     .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Company>()
     .HasOne(c => c.User)
     .WithMany(u => u.Companies)
     .OnDelete(DeleteBehavior.Cascade);
        }
    


    }
}
