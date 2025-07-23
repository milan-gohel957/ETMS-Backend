using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ETMS.Domain.Entities;
using static ETMS.Domain.Enums.Enums;
using Microsoft.AspNetCore.Mvc;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Permission>(entity => entity.HasIndex(e => e.Name).HasDatabaseName("IX_Permissions_Name"));
        
        modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.RoleId, ur.UserId });
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

            
        modelBuilder.Entity<Comment>()
        .HasOne(c => c.User)
        .WithMany()
        .HasForeignKey(c => c.UserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.CreatedByUser)
            .WithMany()
            .HasForeignKey(c => c.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.UpdatedByUser)
            .WithMany()
            .HasForeignKey(c => c.UpdatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<ProjectTask>()
            .HasOne(c => c.CreatedByUser)
            .WithMany()
            .HasForeignKey(c => c.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectTask>()
            .HasOne(c => c.UpdatedByUser)
            .WithMany()
            .HasForeignKey(c => c.UpdatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .Ignore(u => u.CreatedByUser)
            .Ignore(u => u.UpdatedByUser);

        modelBuilder.Entity<Role>()
            .HasOne(r => r.CreatedByUser)
            .WithMany()
            .HasForeignKey(r => r.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Role>()
            .HasOne(r => r.UpdatedByUser)
            .WithMany()
            .HasForeignKey(r => r.UpdatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Milestone>()
              .HasOne(m => m.Project)
              .WithMany()
              .HasForeignKey(m => m.ProjectId)
              .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Milestone>()
            .HasOne(m => m.Status)
            .WithMany() // or .WithMany(s => s.Milestones) if you have that navigation
            .HasForeignKey(m => m.StatusId)
            .OnDelete(DeleteBehavior.Restrict); // THIS is the key!


        //Seed roles
        modelBuilder.Entity<Role>().HasData(
            new Role() { Id = 1, Name = RoleEnum.Admin.ToString(), CreatedAt = DateTime.UtcNow, Description = "This is Admin Role. It will have all the permissions." },
            new Role() { Id = 2, Name = GetEnumDescription(RoleEnum.ProgramManager), CreatedAt = DateTime.UtcNow, Description = "This is Program Manger Role." },
            new Role() { Id = 3, Name = GetEnumDescription(RoleEnum.ProjectManager), CreatedAt = DateTime.UtcNow, Description = "This is Project Manger Role." },
            new Role() { Id = 4, Name = GetEnumDescription(RoleEnum.TeamLead), CreatedAt = DateTime.UtcNow, Description = "This is Team Lead Role." },
            new Role() { Id = 5, Name = GetEnumDescription(RoleEnum.SeniorDeveloper), CreatedAt = DateTime.UtcNow, Description = "This is Senior Developer Role." },
            new Role() { Id = 6, Name = GetEnumDescription(RoleEnum.JuniorDeveloper), CreatedAt = DateTime.UtcNow, Description = "This is Junior Developer Role." },
            new Role() { Id = 7, Name = GetEnumDescription(RoleEnum.User), CreatedAt = DateTime.UtcNow, Description = "This is default User Role." }
        );

        modelBuilder.Entity<UserProjectRole>()
            .HasOne(p => p.Role)
            .WithMany(p => p.UserProjectRoles)
            .HasForeignKey(p => p.RoleId);

        modelBuilder.Entity<UserProjectRole>()
            .HasOne(p => p.User)
            .WithMany(p => p.UserProjectRoles)
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<UserProjectRole>()
            .HasOne(p => p.Project)
            .WithMany(p => p.UserProjectRoles)
            .HasForeignKey(p => p.ProjectId);

    }
}
