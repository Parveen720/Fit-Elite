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

        #region DbSets

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Gym> Gyms => Set<Gym>();
        public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
        public DbSet<MemberSubscription> MemberSubscriptions => Set<MemberSubscription>();
        public DbSet<Payment> Payments => Set<Payment>();

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Global Query Filters

            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Role>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Gym>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<SubscriptionPlan>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<MemberSubscription>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Payment>().HasQueryFilter(x => !x.IsDeleted);

            #endregion

            #region Relationships

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Gym>()
                .HasOne(g => g.GymOwner)
                .WithMany()
                .HasForeignKey(g => g.GymOwnerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubscriptionPlan>()
                .HasOne(sp => sp.Gym)
                .WithMany()
                .HasForeignKey(sp => sp.GymId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberSubscription>()
                .HasOne(ms => ms.Member)
                .WithMany()
                .HasForeignKey(ms => ms.MemberId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberSubscription>()
                .HasOne(ms => ms.Gym)
                .WithMany()
                .HasForeignKey(ms => ms.GymId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberSubscription>()
                .HasOne(ms => ms.SubscriptionPlan)
                .WithMany()
                .HasForeignKey(ms => ms.SubscriptionPlanId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.MemberSubscription)
                .WithMany()
                .HasForeignKey(p => p.MemberSubscriptionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }

        public override int SaveChanges()
        {
            ApplyAuditInformation();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInformation();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditInformation()
        {
            var entries = ChangeTracker
                .Entries<BaseEntity>()
                .Where(e =>
                    e.State == EntityState.Added ||
                    e.State == EntityState.Modified ||
                    e.State == EntityState.Deleted)
                .ToList();

            var currentUser = "System";
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:

                        entry.Entity.CreatedOn = utcNow;
                        entry.Entity.CreatedBy = currentUser;

                        entry.Entity.IsDeleted = false;
                        entry.Entity.ModifiedOn = null;
                        entry.Entity.ModifiedBy = null;
                        entry.Entity.DeletedOn = null;
                        entry.Entity.DeletedBy = null;

                        break;

                    case EntityState.Modified:

                       
                        entry.Property(e => e.CreatedOn).IsModified = false;
                        entry.Property(e => e.CreatedBy).IsModified = false;

                        entry.Entity.ModifiedOn = utcNow;
                        entry.Entity.ModifiedBy = currentUser;

                        break;

                    case EntityState.Deleted:

                       
                        entry.State = EntityState.Modified;

                        entry.Property(e => e.CreatedOn).IsModified = false;
                        entry.Property(e => e.CreatedBy).IsModified = false;

                        entry.Entity.IsDeleted = true;
                        entry.Entity.DeletedOn = utcNow;
                        entry.Entity.DeletedBy = currentUser;

                        break;
                }
            }
        }
    }
}