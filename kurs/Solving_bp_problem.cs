using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kurs.Database
{
    [Table("backpack_problem", Schema = "dbo")] 
    public class BackpackProblem
    {
        public int Id { get; set; }
        public string Task_type { get; set; }
        public int Backpack_weight { get; set; }
        public int Number_of_items { get; set; }
        public int Answer { get; set; }
        public string Items { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm:ss}",
       ApplyFormatInEditMode = true)]
        public DateTime Date_time { get; set; }
    }
    public class AppDbContext : DbContext
    {
        public DbSet<BackpackProblem> BackpackProblems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Указываем связь модели с таблицей
            modelBuilder.Entity<BackpackProblem>()
                .ToTable("backpack_problem", "dbo");
        }
    }
}