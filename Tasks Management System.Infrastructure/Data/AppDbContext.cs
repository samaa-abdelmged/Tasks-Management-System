using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tasks_Management_System.Domain.Entities;

namespace Tasks_Management_System.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Section> Sections { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskShare> TaskShares { get; set; }
        public DbSet<SectionShare> SectionShares { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // =========================
            // User -> Sections
            // =========================
            builder.Entity<Section>()
                .HasOne(s => s.Owner)
                .WithMany(u => u.Sections)
                .HasForeignKey(s => s.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // User -> Tasks
            // =========================
            builder.Entity<TaskItem>()
                .HasOne(t => t.Owner)
                .WithMany(u => u.TaskItems)
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // Section -> Tasks
            // =========================
            builder.Entity<TaskItem>()
                .HasOne(t => t.Section)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.SectionId)
    .OnDelete(DeleteBehavior.Restrict); // بدل Cascade

            // =========================
            // TaskShare relations
            // =========================
            builder.Entity<TaskShare>()
                .HasOne(ts => ts.Task)
                .WithMany(t => t.SharedWithUsers)
                .HasForeignKey(ts => ts.TaskId)
    .OnDelete(DeleteBehavior.Restrict); // بدل Cascade

            builder.Entity<TaskShare>()
                .HasOne(ts => ts.User)
                .WithMany(u => u.SharedTasks)
                .HasForeignKey(ts => ts.UserId)
    .OnDelete(DeleteBehavior.Restrict); // بدل Cascade

            // =========================
            // SectionShare relations
            // =========================
            builder.Entity<SectionShare>()
                .HasOne(ss => ss.Section)
                .WithMany(s => s.SharedWithUsers)
                .HasForeignKey(ss => ss.SectionId)
    .OnDelete(DeleteBehavior.Restrict); // بدل Cascade

            builder.Entity<SectionShare>()
                .HasOne(ss => ss.User)
                .WithMany(u => u.SharedSections)
                .HasForeignKey(ss => ss.UserId)
    .OnDelete(DeleteBehavior.Restrict); // بدل Cascade

            // =========================
            // Unique Index (Prevent Duplicate Sharing)
            // =========================

            builder.Entity<ApplicationUser>()
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Entity<Section>()
              .HasIndex(u => u.Name)
              .IsUnique();

            builder.Entity<TaskItem>()
              .HasIndex(u => u.Title)
              .IsUnique();

            builder.Entity<TaskShare>()
                .HasIndex(ts => new { ts.TaskId, ts.UserId })
                .IsUnique();

            builder.Entity<SectionShare>()
                .HasIndex(ss => new { ss.SectionId, ss.UserId })
                .IsUnique();
        }
    }
}