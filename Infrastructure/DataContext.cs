using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Owner>().Property(x => x.Id).HasDefaultValueSql("NewId()");
            modelBuilder.Entity<PortFolioItem>().Property(x => x.Id).HasDefaultValueSql("NewId()");

            modelBuilder.Entity<Owner>().HasData(
                new Owner()
                {
                    Id=Guid.NewGuid(),
                    FullName="Audai Sam",
                    Avatar="avatar.jpg",
                    Profile="Computer Engenering"
                }
                );
        }

        public DbSet<Owner> Owner { get; set; }
        public DbSet<PortFolioItem> PortFolioItems { get; set; }
    }
}
