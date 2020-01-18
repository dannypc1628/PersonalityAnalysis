namespace PersonalityAnalysis.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<Lecture> Lecture { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Response> Response { get; set; }
        public virtual DbSet<ResponseGoal> ResponseGoal { get; set; }
        public virtual DbSet<ResponseImportanceSort> ResponseImportanceSort { get; set; }
        public virtual DbSet<Result> Result { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<ExportResult_View> ExportResult_View { get; set; }
        public virtual DbSet<Response_View> Response_View { get; set; }
        public virtual DbSet<Result_View> Result_View { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lecture>()
                .HasMany(e => e.Student)
                .WithRequired(e => e.Lecture)
                .HasForeignKey(e => e.Lecture_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Response)
                .WithRequired(e => e.Question)
                .HasForeignKey(e => e.Question_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Response)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.Student_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.ResponseGoal)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.Student_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ID)
                .IsFixedLength();
        }
    }
}
