using FlashCard.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Data
{
    public class FlashCardDbContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=flashCard_data;database=postgres;user id=postgres;password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("QuestionId").StartsAt(2).IncrementsBy(1);
            modelBuilder.Entity<Question>(o => o.HasKey(k => k.QuestionId));
            modelBuilder.Entity<Question>().Property(p => p.QuestionId).HasDefaultValueSql("nextval('\"QuestionId\"')");

            modelBuilder.Entity<Question>().HasData(new Question()
            {
                QuestionId = 1,
                QuestionText = "This is a test question",
                Subject = "Test Subject",
                Difficulty = 1,
                Answer = "Test Answer"
            });
        }
    }
}