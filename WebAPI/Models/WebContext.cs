using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models;

public partial class WebContext : DbContext
{
    public WebContext(DbContextOptions<WebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employee { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<NewsFiles> NewsFiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.Property(e => e.NewsId).HasDefaultValueSql("(newid())", "DF_News_NewsId");
            entity.Property(e => e.EndDateTime)
                .HasDefaultValueSql("(getdate())", "DF_News_EndDateTime")
                .HasColumnType("datetime");
            entity.Property(e => e.InsertDateTime)
                .HasDefaultValueSql("(getdate())", "DF_News_InsertDateTime")
                .HasColumnType("datetime");
            entity.Property(e => e.StartDateTime)
                .HasDefaultValueSql("(getdate())", "DF_News_StartDateTime")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(250);
            entity.Property(e => e.UpdateDateTime)
                .HasDefaultValueSql("(getdate())", "DF_News_UpdateDateTime")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<NewsFiles>(entity =>
        {
            entity.Property(e => e.NewsFilesId).HasDefaultValueSql("(newid())", "DF_NewsFiles_NewsFilesId");
            entity.Property(e => e.Extension).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.NewsId).HasDefaultValueSql("(newid())", "DF_NewsFiles_NewsId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
