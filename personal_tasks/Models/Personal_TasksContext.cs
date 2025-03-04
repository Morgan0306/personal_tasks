using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace personal_tasks.Models;

public partial class Personal_TasksContext : DbContext
{
    public Personal_TasksContext(DbContextOptions<Personal_TasksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departments> Departments { get; set; }

    public virtual DbSet<Notifications> Notifications { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<TaskCategories> TaskCategories { get; set; }

    public virtual DbSet<TaskDelegations> TaskDelegations { get; set; }

    public virtual DbSet<TaskHistory> TaskHistory { get; set; }

    public virtual DbSet<Tasks> Tasks { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<askComments> askComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departments>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED9C25BBD8");

            entity.ToTable(tb => tb.HasTrigger("trg_Departments_Update"));

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Manager).WithMany(p => p.Departments)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK_Departments_Manager");
        });

        modelBuilder.Entity<Notifications>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E12666E1C67");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NotificationType).HasMaxLength(50);

            entity.HasOne(d => d.Task).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_Tasks");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_Users");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A71930001");

            entity.ToTable(tb => tb.HasTrigger("trg_Roles_Update"));

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<TaskCategories>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__TaskCate__19093A0B967D29CE");

            entity.ToTable(tb => tb.HasTrigger("trg_TaskCategories_Update"));

            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<TaskDelegations>(entity =>
        {
            entity.HasKey(e => e.DelegationId).HasName("PK__TaskDele__44488333C07FE7A0");

            entity.ToTable(tb => tb.HasTrigger("trg_TaskDelegations_UpdateRespondedAt"));

            entity.Property(e => e.DelegatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RespondedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.AssignedByNavigation).WithMany(p => p.TaskDelegationsAssignedByNavigation)
                .HasForeignKey(d => d.AssignedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaskDelegations_AssignedBy");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.TaskDelegationsAssignedToNavigation)
                .HasForeignKey(d => d.AssignedTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaskDelegations_AssignedTo");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskDelegations)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaskDelegations_Tasks");
        });

        modelBuilder.Entity<TaskHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__TaskHist__4D7B4ABDA1E72FAF");

            entity.Property(e => e.ChangedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.TaskHistory)
                .HasForeignKey(d => d.ChangedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaskHistory_Users");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskHistory)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaskHistory_Tasks");
        });

        modelBuilder.Entity<Tasks>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949B134342D05");

            entity.ToTable(tb => tb.HasTrigger("trg_Tasks_Update"));

            entity.Property(e => e.CompletedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(150);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Tasks_TaskCategories_Category");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Department).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Tasks_Departments_Department");

            entity.HasOne(d => d.ParentTask).WithMany(p => p.InverseParentTask)
                .HasForeignKey(d => d.ParentTaskId)
                .HasConstraintName("FK_Tasks_ParentTask");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.UserID).HasName("PK__Users__1788CC4C10EC9230");

            entity.ToTable(tb => tb.HasTrigger("trg_Users_Update"));

            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasComment("創立時間")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasComment("部門");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasComment("Email");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasComment("是否在職");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasComment("使用者名字");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(256)
                .HasComment("雜湊後密碼");
            entity.Property(e => e.RoleId).HasComment("權限類別");
            entity.Property(e => e.UpdatedAt)
                .HasComment("更改時間")
                .HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasComment("使用者帳號");

            entity.HasOne(d => d.Department).WithMany(p => p.Users)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Users_Departments");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        modelBuilder.Entity<askComments>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__askComme__C3B4DFCA9FA7BC59");

            entity.ToTable(tb => tb.HasTrigger("trg_askComments_Update"));

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Task).WithMany(p => p.askComments)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_askComments_Tasks");

            entity.HasOne(d => d.User).WithMany(p => p.askComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_askComments_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
