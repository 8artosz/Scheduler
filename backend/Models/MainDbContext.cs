using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
        {

        }
        public MainDbContext(DbContextOptions options) : base (options)
        {

        }

        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<Operation> Operation { get; set; }
        public DbSet<AppUser> User { get; set; }


        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Exercise>(opt =>
            {
                opt.HasKey(e => e.IdExercise);
                opt.Property(e => e.IdExercise).ValueGeneratedOnAdd();
                opt.Property(e => e.Title).IsRequired().HasMaxLength(100);
                opt.Property(e => e.Description).IsRequired().HasMaxLength(100);
                opt.Property(e => e.Status).IsRequired().HasMaxLength(100);
                opt.HasData(
                    new Exercise { IdExercise = 1, Title = "Test", Description = "Test", Status = "Open" }
                    ); ;

            });
            modelBuilder.Entity<Operation>(opt =>
            {
                
                opt.HasKey(e => e.IdOperation);
                opt.Property(e => e.IdOperation).ValueGeneratedOnAdd();
                opt.Property(e => e.Description).IsRequired();
                opt.Property(e => e.TimeSpent).IsRequired();
                opt.HasData(
                    new Operation { IdOperation = 1, Description = "1", TimeSpent = 1, IdExercise = 1 },
                    new Operation { IdOperation = 2, Description = "2", TimeSpent = 2, IdExercise = 1 }
                    );
                opt.HasOne(d => d.Exercise)
                   .WithMany(p => p.Operations)
                   .HasForeignKey(p => p.IdExercise);

            });

            modelBuilder.Entity<AppUser>(opt =>
            {
                opt.HasKey(e => e.IdUser);
                opt.Property(e => e.IdUser).ValueGeneratedOnAdd();

            });


        }
    }
}
