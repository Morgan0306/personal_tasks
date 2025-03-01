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

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.UserID).HasName("PK__Users__1788CCACE5A3E4CC");

            entity.HasIndex(e => e.UserName, "UQ_Users_UserName").IsUnique();

            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
