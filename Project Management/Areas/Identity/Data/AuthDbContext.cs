using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Project_Management.Models;

namespace Project_Management.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        public DbSet<Institute> Institute { get; set; }
        public DbSet<LabUseItem> LabUseItem { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Depertment> Depertments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Depertment>().HasOne(p => p.Institute)
                .WithMany(b => b.Depertment).HasForeignKey(b => b.InstituteId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
