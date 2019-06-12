using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public sealed class AdvertisementDbContext : IdentityDbContext<User>
    {
        public AdvertisementDbContext(DbContextOptions<AdvertisementDbContext> options) : base(options)
        {
            // TODO Add seeding admin
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
                .HasData(
                    // TODO to constants
                    new IdentityRole
                    {
                        Id = Role.Admin.ToString(),
                        Name = Role.Admin.ToString(),
                        NormalizedName = Role.Admin.ToString().ToUpper()
                    },
                    new IdentityRole
                    {
                        Id = Role.User.ToString(),
                        NormalizedName = Role.Manager.ToString().ToUpper(),
                        Name = Role.User.ToString()
                    },
                    new IdentityRole
                    {
                        NormalizedName = Role.Manager.ToString().ToUpper(),
                        Id = Role.Manager.ToString(),
                        Name = Role.Manager.ToString()
                    });
        }
    }
}
