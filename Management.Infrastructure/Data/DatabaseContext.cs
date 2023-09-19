using Management.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Infrastructure.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {
                
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Management.Core.Entities.Task> Tasks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Project> Projects { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Project>()
                .HasMany(a => a.Task)
                .WithOne(b => b.Project)
                .HasForeignKey(b => b.ProjectId);

            modelBuilder.Entity<User>()
                .HasMany(a => a.Task)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);*/
        }

    }
}
