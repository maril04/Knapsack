using Microsoft.EntityFrameworkCore;

namespace dbo
{
    // Определение модели данных
    public class backpack_problem
    {
        public int Id { get; set; }

        public string Task_type { get; set; }

        public int Backpack_weight { get; set; }
        public int Number_of_items { get; set; }
        public int Answer { get; set; }
        public string Items { get; set; }
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
    }
}
