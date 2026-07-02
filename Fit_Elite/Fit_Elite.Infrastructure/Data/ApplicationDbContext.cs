using Fit_Elite.Domain.common;
using Fit_Elite.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fit_Elite.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Gym> Gyms => Set<Gym>();
        public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
        public DbSet<MemberSubscription> MemberSubscriptions => Set<MemberSubscription>();
        public DbSet<Payment> Payments => Set<Payment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        
            // SOFT-DELETE QUERY FILTERS
          
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Gym>().HasQueryFilter(g => !g.IsDeleted);
            modelBuilder.Entity<SubscriptionPlan>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<MemberSubscription>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Payment>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<UserRole>().HasQueryFilter(ur => !ur.User.IsDeleted);

            // UNIQUE CONSTRAINTS

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<SubscriptionPlan>()
                .HasIndex(p => new { p.GymId, p.PlanName })
                .IsUnique(); 


            modelBuilder.Entity<UserRole>()
                .HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithOne(x => x.UserRole)
                .HasForeignKey<UserRole>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict); 

           

            modelBuilder.Entity<Gym>()
                .HasOne(x => x.GymOwner)
                .WithOne()
                .HasForeignKey<Gym>(x => x.GymOwnerId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<SubscriptionPlan>()
                .HasOne(x => x.Gym)
                .WithMany()
                .HasForeignKey(x => x.GymId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<MemberSubscription>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberSubscription>()
                .HasOne(x => x.SubscriptionPlan)
                .WithMany()
                .HasForeignKey(x => x.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Payment>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Payment>()
                .HasOne(x => x.SubscriptionPlan)
                .WithMany()
                .HasForeignKey(x => x.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedOn = DateTimeOffset.UtcNow;
                }
            }
        }
    }
}