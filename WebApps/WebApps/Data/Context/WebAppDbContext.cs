using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps.Domain.Models;

namespace WebApps.Data.Context
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Domain.Models.Type> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.Entity<UserType>().HasKey(ur => new { ur.UserId, ur.TypeId });
        }
    }
}
