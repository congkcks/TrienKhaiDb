using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VocabularyFinal.Models;

namespace VocabularyFinal.Data;

public partial class LuyenTuVungDbContext : DbContext
{
    public LuyenTuVungDbContext()
    {
    }

    public LuyenTuVungDbContext(DbContextOptions<LuyenTuVungDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<flashcard> flashcards { get; set; }

    public virtual DbSet<vocab_group> vocab_groups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=dpg-d3rq5mngi27c73f9nmag-a.singapore-postgres.render.com;Port=5432;Database=luyentatuvung_kvbv;Username=luyentatuvung_kvbv_user;Password=QBZhXJPAzezzfvZayn8q5oi9HEMKh97G;SSL Mode=Require;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<flashcard>(entity =>
        {
            entity.HasKey(e => e.id).HasName("flashcards_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(d => d.group).WithMany(p => p.flashcards)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("flashcards_group_id_fkey");
        });

        modelBuilder.Entity<vocab_group>(entity =>
        {
            entity.HasKey(e => e.id).HasName("vocab_groups_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
