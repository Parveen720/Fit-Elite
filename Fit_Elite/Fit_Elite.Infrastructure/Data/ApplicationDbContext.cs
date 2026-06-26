using Fit_Elite.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Fit_Elite.Infrastructure.Data
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<Gym>().HasQueryFilter(g => !g.IsDeleted);
            modelBuilder.Entity<SubscriptionPlan>().HasQueryFilter(sp => !sp.IsDeleted);
            modelBuilder.Entity<MemberSubscription>().HasQueryFilter(ms => !ms.IsDeleted);
            modelBuilder.Entity<Payment>().HasQueryFilter(p => !p.IsDeleted);
        }

        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

       
        public override int SaveChanges()
        {
            ApplyAuditInfo();
            return base.SaveChanges();
        }


        private void ApplyAuditInfo()
        {
            
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity &&
                      (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
                .ToList(); 

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
        }
    }
}