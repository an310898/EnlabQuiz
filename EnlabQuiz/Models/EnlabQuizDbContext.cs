using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EnlabQuiz.Models;

public partial class EnlabQuizDbContext : DbContext
{
    public EnlabQuizDbContext()
    {
    }

    public EnlabQuizDbContext(DbContextOptions<EnlabQuizDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HistoryPlay> HistoryPlays { get; set; }

    public virtual DbSet<PlayQuiz> PlayQuizzes { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=EnlabQuizDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HistoryPlay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HistoryP__3214EC072662559D");

            entity.ToTable("HistoryPlay");

            entity.Property(e => e.Choice)
                .HasMaxLength(2)
                .IsUnicode(false);

            entity.HasOne(d => d.Play).WithMany(p => p.HistoryPlays)
                .HasForeignKey(d => d.PlayId)
                .HasConstraintName("FK__HistoryPl__PlayI__3C69FB99");

            entity.HasOne(d => d.Quiz).WithMany(p => p.HistoryPlays)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("FK__HistoryPl__QuizI__3D5E1FD2");
        });

        modelBuilder.Entity<PlayQuiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PlayQuiz__3214EC079201E38C");

            entity.ToTable("PlayQuiz");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quiz__3214EC07B80A5C9B");

            entity.ToTable("Quiz");

            entity.Property(e => e.Answer).HasMaxLength(1000);
            entity.Property(e => e.Correct).HasMaxLength(100);
            entity.Property(e => e.Question).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
