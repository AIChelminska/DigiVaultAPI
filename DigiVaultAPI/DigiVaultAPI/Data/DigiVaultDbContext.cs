using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiVaultAPI.Data;

public class DigiVaultDbContext : DbContext
{
    public DigiVaultDbContext(DbContextOptions<DigiVaultDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<CourseReport> CourseReports { get; set; }
    public DbSet<PlatformSettings> PlatformSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ── USER ──────────────────────────────────────────────────────────────
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.IdUser);

            entity.HasIndex(u => u.Login).IsUnique();
            entity.HasIndex(u => u.Email).IsUnique();

            entity.Property(u => u.Login).HasMaxLength(50).IsRequired();
            entity.Property(u => u.Email).HasMaxLength(100).IsRequired();
            entity.Property(u => u.PasswordHash).HasMaxLength(256).IsRequired();
            entity.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
            entity.Property(u => u.LastName).HasMaxLength(50).IsRequired();
            entity.Property(u => u.Balance).HasColumnType("decimal(10,2)");
            entity.Property(u => u.TotalWithdrawn).HasColumnType("decimal(10,2)");
            entity.Property(u => u.Role).HasConversion<int>();
        });

        // ── CATEGORY ──────────────────────────────────────────────────────────
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.IdCategory);
            entity.Property(c => c.Name).HasMaxLength(100).IsRequired();
        });

        // ── COURSE ────────────────────────────────────────────────────────────
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(c => c.IdCourse);

            entity.Property(c => c.Title).HasMaxLength(200).IsRequired();
            entity.Property(c => c.Description).IsRequired();
            entity.Property(c => c.Price).HasColumnType("decimal(10,2)");
            entity.Property(c => c.ImageUrl).HasMaxLength(500);
            entity.Property(c => c.AverageRating).HasColumnType("decimal(3,2)");
            entity.Property(c => c.CreatedAt).HasDefaultValueSql("NOW()");

            entity.HasOne(c => c.User)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.Category)
                .WithMany(cat => cat.Courses)
                .HasForeignKey(c => c.IdCategory)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ── ORDER ─────────────────────────────────────────────────────────────
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.IdOrder);

            entity.Property(o => o.TotalPrice).HasColumnType("decimal(10,2)");
            entity.Property(o => o.CreatedAt).HasDefaultValueSql("NOW()");

            entity.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ── ORDER ITEM ────────────────────────────────────────────────────────
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(oi => oi.IdOrderItem);

            entity.Property(oi => oi.Price).HasColumnType("decimal(10,2)");
            entity.Property(oi => oi.CommissionRate).HasColumnType("decimal(5,4)");

            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.IdOrder)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(oi => oi.Course)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(oi => oi.IdCourse)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ── USER COURSE ───────────────────────────────────────────────────────
        modelBuilder.Entity<UserCourse>(entity =>
        {
            entity.HasKey(uc => uc.IdUserCourse);

            entity.HasIndex(uc => new { uc.IdUser, uc.IdCourse }).IsUnique();

            entity.Property(uc => uc.PurchasedAt).HasDefaultValueSql("NOW()");

            entity.HasOne(uc => uc.User)
                .WithMany(u => u.UserCourses)
                .HasForeignKey(uc => uc.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(uc => uc.Course)
                .WithMany(c => c.UserCourses)
                .HasForeignKey(uc => uc.IdCourse)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ── CART ITEM ─────────────────────────────────────────────────────────
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(ci => ci.IdCartItem);

            entity.HasIndex(ci => new { ci.IdUser, ci.IdCourse }).IsUnique();

            entity.Property(ci => ci.AddedAt).HasDefaultValueSql("NOW()");

            entity.HasOne(ci => ci.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(ci => ci.IdUser)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ci => ci.Course)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.IdCourse)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ── WISHLIST ITEM ─────────────────────────────────────────────────────
        modelBuilder.Entity<WishlistItem>(entity =>
        {
            entity.HasKey(wi => wi.IdWishlistItem);

            entity.HasIndex(wi => new { wi.IdUser, wi.IdCourse }).IsUnique();

            entity.Property(wi => wi.AddedAt).HasDefaultValueSql("NOW()");

            entity.HasOne(wi => wi.User)
                .WithMany(u => u.WishlistItems)
                .HasForeignKey(wi => wi.IdUser)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(wi => wi.Course)
                .WithMany(c => c.WishlistItems)
                .HasForeignKey(wi => wi.IdCourse)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ── REVIEW ────────────────────────────────────────────────────────────
        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(r => r.IdReview);

            entity.HasIndex(r => new { r.IdUser, r.IdCourse }).IsUnique();

            entity.Property(r => r.Comment).HasMaxLength(1000);
            entity.Property(r => r.CreatedAt).HasDefaultValueSql("NOW()");

            entity.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Course)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.IdCourse)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ── NOTIFICATION ──────────────────────────────────────────────────────
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(n => n.IdNotification);

            entity.Property(n => n.Title).HasMaxLength(200).IsRequired();
            entity.Property(n => n.Message).HasMaxLength(1000).IsRequired();
            entity.Property(n => n.CreatedAt).HasDefaultValueSql("NOW()");

            entity.HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ── COURSE REPORT ─────────────────────────────────────────────────────
        modelBuilder.Entity<CourseReport>(entity =>
        {
            entity.HasKey(cr => cr.IdCourseReport);

            entity.HasIndex(cr => new { cr.IdUser, cr.IdCourse }).IsUnique();

            entity.Property(cr => cr.Reason).HasMaxLength(1000).IsRequired();
            entity.Property(cr => cr.CreatedAt).HasDefaultValueSql("NOW()");

            entity.HasOne(cr => cr.User)
                .WithMany(u => u.CourseReports)
                .HasForeignKey(cr => cr.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(cr => cr.Course)
                .WithMany(c => c.CourseReports)
                .HasForeignKey(cr => cr.IdCourse)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ── PLATFORM SETTINGS ─────────────────────────────────────────────────
        modelBuilder.Entity<PlatformSettings>(entity =>
        {
            entity.HasKey(ps => ps.IdPlatformSettings);

            entity.Property(ps => ps.CommissionRate).HasColumnType("decimal(5,4)");
            entity.Property(ps => ps.PlatformBalance).HasColumnType("decimal(14,2)");
        });
    }
}
