using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dbo
{
    [Table("backpack_problem", Schema = "dbo")]
    public class backpack_problem
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
    public class ApplicationContext : DbContext
    {
        // DbSet представляет собой список сущностей, хранящихся в базе данных
        public DbSet<backpack_problem> backpack_problem { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            // Создание БД, если она отсутствует
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Указываем связь модели с таблицей
            modelBuilder.Entity<backpack_problem>()
                .ToTable("backpack_problem", "dbo");
        }
    }
}