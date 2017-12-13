using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace esencialAdmin.Data.Models
{
    public partial class esencialAdminContext : DbContext
    {
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<PaymentMethods> PaymentMethods { get; set; }
        public virtual DbSet<Periodes> Periodes { get; set; }
        public virtual DbSet<PeriodesGoodies> PeriodesGoodies { get; set; }
        public virtual DbSet<PlanGoodies> PlanGoodies { get; set; }
        public virtual DbSet<Plans> Plans { get; set; }
        public virtual DbSet<Subscription> Subscription { get; set; }
        public virtual DbSet<SubscriptionPhotos> SubscriptionPhotos { get; set; }
        public virtual DbSet<SubscriptionStatus> SubscriptionStatus { get; set; }
        public virtual DbSet<Templates> Templates { get; set; }

        private string _user;
        public esencialAdminContext(DbContextOptions<esencialAdminContext> options, Services.UserResolverService userService)
                 : base(options)
        {
            _user = userService.GetUser();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(256);

                entity.Property(e => e.LastName).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("idx_customer_email_notnull")
                    .IsUnique()
                    .HasFilter("([Email] IS NOT NULL)");

                entity.Property(e => e.City).HasMaxLength(60);

                entity.Property(e => e.Company).HasMaxLength(256);

                entity.Property(e => e.Email)
                    .HasColumnName("EMail")
                    .HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(25);

                entity.Property(e => e.Street).HasMaxLength(256);

                entity.Property(e => e.Title).HasMaxLength(20);

                entity.Property(e => e.UserCreated).HasMaxLength(450);

                entity.Property(e => e.UserModified).HasMaxLength(450);

                entity.Property(e => e.Zip).HasMaxLength(11);
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OriginalName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(1024);

                entity.Property(e => e.UserCreated).HasMaxLength(450);

                entity.Property(e => e.UserModified).HasMaxLength(450);
            });

            modelBuilder.Entity<PaymentMethods>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Periodes>(entity =>
            {
                entity.Property(e => e.FkGiftedById).HasColumnName("fk_GiftedById");

                entity.Property(e => e.FkPayedMethodId).HasColumnName("fk_PayedMethodId");

                entity.Property(e => e.FkSubscriptionId).HasColumnName("fk_SubscriptionId");

                entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.StartDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserCreated).HasMaxLength(450);

                entity.Property(e => e.UserModified).HasMaxLength(450);

                entity.HasOne(d => d.FkGiftedBy)
                    .WithMany(p => p.Periodes)
                    .HasForeignKey(d => d.FkGiftedById)
                    .HasConstraintName("FK_PeridoesGiftedBy_Customer_Id");

                entity.HasOne(d => d.FkPayedMethod)
                    .WithMany(p => p.Periodes)
                    .HasForeignKey(d => d.FkPayedMethodId)
                    .HasConstraintName("FK_Periodes_PaymentMethods_Id");

                entity.HasOne(d => d.FkSubscription)
                    .WithMany(p => p.Periodes)
                    .HasForeignKey(d => d.FkSubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Periodes_Subscription_Id");
            });

            modelBuilder.Entity<PeriodesGoodies>(entity =>
            {
                entity.Property(e => e.FkPeriodesId).HasColumnName("fk_PeriodesId");

                entity.Property(e => e.FkPlanGoodiesId).HasColumnName("fk_PlanGoodiesId");

                entity.Property(e => e.Received).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserCreated).HasMaxLength(450);

                entity.Property(e => e.UserModified).HasMaxLength(450);

                entity.HasOne(d => d.FkPeriodes)
                    .WithMany(p => p.PeriodesGoodies)
                    .HasForeignKey(d => d.FkPeriodesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeriodesGoodies_Periodes_Id");

                entity.HasOne(d => d.FkPlanGoodies)
                    .WithMany(p => p.PeriodesGoodies)
                    .HasForeignKey(d => d.FkPlanGoodiesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeriodesGoodies_PlanGoodies_Id");
            });

            modelBuilder.Entity<PlanGoodies>(entity =>
            {
                entity.Property(e => e.Bezeichnung)
                    .IsRequired()
                    .HasMaxLength(128);
    
                entity.Property(e => e.FkTemplateLabel).HasColumnName("fk_templateLabel");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.FkTemplateLabelNavigation)
                    .WithMany(p => p.PlanGoodies)
                    .HasForeignKey(d => d.FkTemplateLabel)
                    .HasConstraintName("FK_PlanGoodies_Templates_Id");
            });

            modelBuilder.Entity<Plans>(entity =>
            {
                entity.Property(e => e.Deadline).HasColumnType("date");

                entity.Property(e => e.FkGoodyId).HasColumnName("fk_GoodyId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.UserCreated).HasMaxLength(450);

                entity.Property(e => e.UserModified).HasMaxLength(450);

                entity.HasOne(d => d.FkGoody)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.FkGoodyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Plans_PlanGoodies_Id");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.Property(e => e.FkCustomerId).HasColumnName("fk_CustomerId");

                entity.Property(e => e.FkPlanId).HasColumnName("fk_PlanId");

                entity.Property(e => e.FkSubscriptionStatus).HasColumnName("fk_SubscriptionStatus");

                entity.Property(e => e.UserCreated).HasMaxLength(450);

                entity.Property(e => e.UserModified).HasMaxLength(450);

                entity.HasOne(d => d.FkCustomer)
                    .WithMany(p => p.Subscription)
                    .HasForeignKey(d => d.FkCustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subscription_Customer_Id");

                entity.HasOne(d => d.FkPlan)
                    .WithMany(p => p.Subscription)
                    .HasForeignKey(d => d.FkPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subscription_Plans_Id");

                entity.HasOne(d => d.FkSubscriptionStatusNavigation)
                    .WithMany(p => p.Subscription)
                    .HasForeignKey(d => d.FkSubscriptionStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subscription_SubscriptionStatus_Id");
            });

            modelBuilder.Entity<SubscriptionPhotos>(entity =>
            {
                entity.Property(e => e.FkFileId).HasColumnName("fk_FileId");

                entity.Property(e => e.FkSubscriptionId).HasColumnName("fk_SubscriptionId");

                entity.Property(e => e.UserCreated).HasMaxLength(450);

                entity.Property(e => e.UserModified).HasMaxLength(450);

                entity.HasOne(d => d.FkFile)
                    .WithMany(p => p.SubscriptionPhotos)
                    .HasForeignKey(d => d.FkFileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubscriptionPhotos_File_Id");

                entity.HasOne(d => d.FkSubscription)
                    .WithMany(p => p.SubscriptionPhotos)
                    .HasForeignKey(d => d.FkSubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubscriptionPhotos_Subscription_Id");
            });

            modelBuilder.Entity<SubscriptionStatus>(entity =>
            {
                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Templates>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);
            });
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is ITrackableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = !string.IsNullOrEmpty(_user)
                ? _user
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((ITrackableEntity)entity.Entity).DateCreated = DateTime.UtcNow;
                    ((ITrackableEntity)entity.Entity).UserCreated = currentUsername;
                }

                ((ITrackableEntity)entity.Entity).DateModified = DateTime.UtcNow;
                ((ITrackableEntity)entity.Entity).UserModified = currentUsername;
            }
        }
    }
}
