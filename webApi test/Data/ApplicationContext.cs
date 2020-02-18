using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using webApi_test.Models;

namespace webApi_test.Data
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.Entity<ResourceModel>()
                .HasMany(p => p.Children)
                .WithOne(p => p.parent)
                .HasForeignKey(p => p.parentId);    

               
        }

        public DbSet<ResourceTypeModel> ResourceType { get; set; }
        public DbSet<ResourceModel> Resources { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }

    }
}
