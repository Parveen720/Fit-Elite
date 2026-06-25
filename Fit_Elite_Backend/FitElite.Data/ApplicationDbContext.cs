using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FitElite.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FitElite.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<MemberSubscription> MemberSubscriptions { get; set; }
        public DbSet<Payment> Payments { get; set; }

        // --- Automated Audit Interceptor ---
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity &&
                      (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;
                string currentSystemUser = "System"; 

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                    entity.CreatedBy = currentSystemUser;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                    entity.ModifiedBy = currentSystemUser;
                }
                else if (entry.State == EntityState.Deleted)
                {
                   
                    entry.State = EntityState.Modified;
                    entity.IsDeleted = true;
                    entity.DeletedOn = DateTime.UtcNow;
                    entity.DeletedBy = currentSystemUser;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}