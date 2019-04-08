using Microsoft.Data.Entity;
////using Microsoft.EntityFrameworkCore;
using System.Data.Entity;


namespace GeekQuiz.Models
{
    public class TriviaDbContext : DbContext
    {
        private static bool _created = false;

        public TriviaDbContext()
        {
            if (!_created)
            {
                _created = true;
                Database.CreateIfNotExists();
            }
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<TriviaOption>()
                .HasKey(o => new { o.QuestionId, o.Id });

            builder.Entity<TriviaAnswer>()
                .HasRequired(a => a.TriviaOption)
                .WithMany()
                .HasForeignKey(a => new { a.QuestionId, a.OptionId });

            builder.Entity<TriviaQuestion>()
                .HasMany(q => q.Options)
                .WithRequired(o => o.TriviaQuestion);
        }
        
        public DbSet<TriviaQuestion> TriviaQuestions { get; set; }

        public DbSet<TriviaOption> TriviaOptions { get; set; }

        public DbSet<TriviaAnswer> TriviaAnswers { get; set; }
    }
}
