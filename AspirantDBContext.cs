using AspirantDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AspirantDatabase
{
    public class AspirantDBContext : DbContext
    {
        const string ConnectionString = @"Server=ORANGEVM-PC\SQLEXPRESS;Database=Aspirant;User Id=sa;Password=123456;";

        public AspirantDBContext() { }

        public AspirantDBContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PassingExam>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Abiturient> Abiturients { get; set; }

        public DbSet<Abstract> Abstracts { get; set; }

        public DbSet<Aspirant> Aspirants { get; set; }

        public DbSet<CandidateExam> CandidateExams { get; set; }

        public DbSet<Cathedra> Cathedras { get; set; }

        public DbSet<Conference> Conferences { get; set; }

        public DbSet<Diplom> Diploms { get; set; }

        public DbSet<Enducation> Enducations { get; set; }

        public DbSet<EntryExam> EntryExams { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<PassingCandidateExam> PassingCandidateExams { get; set; }

        public DbSet<PassingEntryExam> PassingEntryExams { get; set; }

        public DbSet<PassingExam> PassingExams { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<PrelProtection> PrelProtections { get; set; }

        public DbSet<Protection> Protections { get; set; }

        public DbSet<Publication> Publications { get; set; }

        public DbSet<Specialty> Specialties { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
