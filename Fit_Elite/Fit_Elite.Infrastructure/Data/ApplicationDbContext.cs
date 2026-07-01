using Fit_Elite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

           
            // USER_ROLE (One User -> One Role)
     

            modelBuilder.Entity<UserRole>()
                .HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithOne(x => x.UserRole)
                .HasForeignKey<UserRole>(x => x.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId);

            // GYM (One Gym -> One Owner)
         

            modelBuilder.Entity<Gym>()
                .HasOne(x => x.GymOwner)
                .WithOne()
                .HasForeignKey<Gym>(x => x.GymOwnerId)
                .OnDelete(DeleteBehavior.Restrict);

        
            // GYM -> SUBSCRIPTION PLAN
          

            modelBuilder.Entity<SubscriptionPlan>()
                .HasOne(x => x.Gym)
                .WithMany()
                .HasForeignKey(x => x.GymId)
                .OnDelete(DeleteBehavior.Cascade);

            
            // USER -> MEMBER SUBSCRIPTION
            

            modelBuilder.Entity<MemberSubscription>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

           
            // PLAN -> MEMBER SUBSCRIPTION
          

            modelBuilder.Entity<MemberSubscription>()
                .HasOne(x => x.SubscriptionPlan)
                .WithMany()
                .HasForeignKey(x => x.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.Restrict);

            
            // USER -> PAYMENT
          

            modelBuilder.Entity<Payment>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}