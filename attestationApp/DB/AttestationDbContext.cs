using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using attestationApp.Models;

namespace attestationApp.DB;

public partial class AttestationDbContext : DbContext
{
    public AttestationDbContext()
    {
    }

    public AttestationDbContext(DbContextOptions<AttestationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Direction> Directions { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=DESKTOP-LDT38S4\\SQLEXPRESS;initial catalog=AttestationDb;trusted_connection=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Answer__3214EC270A9C1B4E");

            entity.ToTable("Answer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.Text).HasMaxLength(500);

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__Answer__Question__4BAC3F29");
        });

        modelBuilder.Entity<Direction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Directio__3214EC27DC7BEEB2");

            entity.ToTable("Direction");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Gender__3214EC27010041B4");

            entity.ToTable("Gender");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mark__3214EC27C9A3C055");

            entity.ToTable("Mark");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Option__3214EC277F5A25AC");

            entity.ToTable("Option");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC27E87E751C");

            entity.ToTable("Question");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.QuestionText)
                .HasMaxLength(500)
                .HasColumnName("Question");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Result__3214EC27ECD5BC80");

            entity.ToTable("Result");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.DirectionId).HasColumnName("DirectionID");
            entity.Property(e => e.MarkId).HasColumnName("MarkID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.TestId).HasColumnName("TestID");

            entity.HasOne(d => d.Direction).WithMany(p => p.Results)
                .HasForeignKey(d => d.DirectionId)
                .HasConstraintName("FK__Result__Directio__5165187F");

            entity.HasOne(d => d.Mark).WithMany(p => p.Results)
                .HasForeignKey(d => d.MarkId)
                .HasConstraintName("FK__Result__MarkID__5070F446");

            entity.HasOne(d => d.Student).WithMany(p => p.Results)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Result__StudentI__4E88ABD4");

            entity.HasOne(d => d.Test).WithMany(p => p.Results)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__Result__TestID__4F7CD00D");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC277596432B");

            entity.ToTable("Student");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Birthdate).HasColumnType("date");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Patronymic).HasMaxLength(100);

            entity.HasOne(d => d.Gender).WithMany(p => p.Students)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK__Student__GenderI__398D8EEE");

            entity.HasMany(d => d.Directions).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentDirection",
                    r => r.HasOne<Direction>().WithMany()
                        .HasForeignKey("DirectionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__StudentDi__Direc__412EB0B6"),
                    l => l.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__StudentDi__Stude__403A8C7D"),
                    j =>
                    {
                        j.HasKey("StudentId", "DirectionId").HasName("PK__StudentD__4AB3AE1BC0EA1113");
                        j.ToTable("StudentDirection");
                        j.IndexerProperty<int>("StudentId").HasColumnName("StudentID");
                        j.IndexerProperty<int>("DirectionId").HasColumnName("DirectionID");
                    });
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Test__3214EC27D8883696");

            entity.ToTable("Test");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.OptionId).HasColumnName("OptionID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

            entity.HasOne(d => d.Option).WithMany(p => p.Tests)
                .HasForeignKey(d => d.OptionId)
                .HasConstraintName("FK__Test__OptionID__47DBAE45");

            entity.HasOne(d => d.Question).WithMany(p => p.Tests)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__Test__QuestionID__48CFD27E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
