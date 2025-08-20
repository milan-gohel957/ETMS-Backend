using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ETMS.Domain.Entities;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Repository.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Milestone> MileStones { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<ProjectUser> ProjectUsers { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<ProjectTask> Tasks { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // CRITICAL: Configure relationships BEFORE ApplyConfigurationsFromAssembly


        // 1. UserTask configuration
        modelBuilder.Entity<UserTask>()
            .HasKey(ut => new { ut.UserId, ut.ProjectTaskId });

        modelBuilder.Entity<UserTask>()
            .HasOne(ut => ut.User)
            .WithMany(u => u.UserTasks)
            .HasForeignKey(ut => ut.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserTask>()
            .HasOne(ut => ut.ProjectTask)
            .WithMany(t => t.UserTasks)
            .HasForeignKey(ut => ut.ProjectTaskId)
            .OnDelete(DeleteBehavior.Restrict);

        // 2. ProjectTask configuration - EXPLICIT AND COMPLETE
        modelBuilder.Entity<ProjectTask>(entity =>
        {
            entity.ToTable("Tasks");

            entity.Property(t => t.ProjectId).HasDefaultValue(1);

            entity.HasOne(t => t.Board)
                .WithMany(b => b.Tasks)
                .HasForeignKey(t => t.BoardId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Status)
                .WithMany()
                .HasForeignKey(t => t.StatusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.CreatedByUser)
                .WithMany()
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.UpdatedByUser)
                .WithMany()
                .HasForeignKey(t => t.UpdatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(t => t.Comments)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(t => t.Attachments)
                .WithOne(a => a.Task)
                .HasForeignKey(a => a.ProjectTaskId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(t => t.Project)
                .WithMany()
                .HasForeignKey(t => t.ProjectId) // Assuming 'ProjectId' is the foreign key
                .OnDelete(DeleteBehavior.Restrict);
        });

        // 3. Board configuration
        modelBuilder.Entity<Board>(entity =>
        {
            entity.HasOne(b => b.Project)
                .WithMany(p => p.Boards)
                .HasForeignKey(b => b.ProjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // CHANGED TO RESTRICT

            entity.HasOne(b => b.CreatedByUser)
                .WithMany()
                .HasForeignKey(b => b.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(b => b.UpdatedByUser)
                .WithMany()
                .HasForeignKey(b => b.UpdatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // 4. Project configuration
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasOne(p => p.Status)
                .WithMany()
                .HasForeignKey(p => p.StatusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(p => p.Comments)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        });



        // 6. UserRole configuration
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.RoleId, ur.UserId });

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Restrict);


        // 8. Comment configuration
        // 5. COMMENT CONFIGURATION - For polymorphic relationships
        modelBuilder.Entity<Comment>(entity =>
        {
            // Comment can belong to EITHER Project OR Task
            entity.HasOne(c => c.Project)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.ProjectTask)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.ProjectTaskId)
                .OnDelete(DeleteBehavior.Cascade);

            // User relationship (who made the comment)
            entity.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Audit relationships
            entity.HasOne(c => c.CreatedByUser)
                .WithMany()
                .HasForeignKey(c => c.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.UpdatedByUser)
                .WithMany()
                .HasForeignKey(c => c.UpdatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);


        });

        // 6. ATTACHMENT CONFIGURATION - Similar polymorphic pattern
        modelBuilder.Entity<Attachment>(entity =>
        {

            entity.HasOne(a => a.Task)
                .WithMany(t => t.Attachments)
                .HasForeignKey(a => a.ProjectTaskId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // User who uploaded
            entity.HasOne(a => a.UploadedBy)
                .WithMany()
                .HasForeignKey(a => a.UploadedByUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Audit relationships
            entity.HasOne(a => a.CreatedByUser)
                .WithMany()
                .HasForeignKey(a => a.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(a => a.UpdatedByUser)
                .WithMany()
                .HasForeignKey(a => a.UpdatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

        });
        // 9. Milestone configuration
        modelBuilder.Entity<Milestone>(entity =>
        {
            entity.HasOne(m => m.Project)
                .WithMany(p => p.Milestones)
                .HasForeignKey(m => m.ProjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // CHANGED TO RESTRICT

            entity.HasOne(m => m.Status)
                .WithMany()
                .HasForeignKey(m => m.StatusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        // 10. UserProjectRole configuration
        modelBuilder.Entity<UserProjectRole>(entity =>
        {
            entity.HasOne(p => p.Role)
                .WithMany(p => p.UserProjectRoles)
                .HasForeignKey(p => p.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.User)
                .WithMany(p => p.UserProjectRoles)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Project)
                .WithMany(p => p.UserProjectRoles)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

        });


        // 11. Permission configuration
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasIndex(e => e.Name)
                .IsUnique()
                .HasDatabaseName("IX_Permissions_Name");
        });



        // NOW apply assembly configurations (if any)
        // Put this AFTER manual configurations to prevent overrides
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Seed data goes LAST
        modelBuilder.Entity<Status>().HasData(
            new Status() { Id = 1, Name = "Pending", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Status() { Id = 2, Name = "Completed", IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );

        modelBuilder.Entity<Role>().HasData(
            new Role() { Id = 1, Name = RoleEnum.Admin.ToString(), CreatedAt = DateTime.UtcNow, Description = "This is Admin Role. It will have all the permissions." },
            new Role() { Id = 2, Name = GetEnumDescription(RoleEnum.ProgramManager), CreatedAt = DateTime.UtcNow, Description = "This is Program Manger Role." },
            new Role() { Id = 3, Name = GetEnumDescription(RoleEnum.ProjectManager), CreatedAt = DateTime.UtcNow, Description = "This is Project Manger Role." },
            new Role() { Id = 4, Name = GetEnumDescription(RoleEnum.TeamLead), CreatedAt = DateTime.UtcNow, Description = "This is Team Lead Role." },
            new Role() { Id = 5, Name = GetEnumDescription(RoleEnum.SeniorDeveloper), CreatedAt = DateTime.UtcNow, Description = "This is Senior Developer Role." },
            new Role() { Id = 6, Name = GetEnumDescription(RoleEnum.JuniorDeveloper), CreatedAt = DateTime.UtcNow, Description = "This is Junior Developer Role." },
            new Role() { Id = 7, Name = GetEnumDescription(RoleEnum.User), CreatedAt = DateTime.UtcNow, Description = "This is default User Role." }
        );
    }
}