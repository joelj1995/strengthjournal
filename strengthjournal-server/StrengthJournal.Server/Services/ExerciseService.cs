using Microsoft.EntityFrameworkCore;
using StrengthJournal.DataAccess.Contexts;
using StrengthJournal.Server.ApiModels;

namespace StrengthJournal.Server.Services
{
    public class ExerciseService
    {
        protected readonly StrengthJournalContext context;

        public ExerciseService(StrengthJournalContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ExerciseDto>> GetExercises()
        {
            var exercises = await context.Exercises.Where(e => e.CreatedByUser == null).ToListAsync();
            return exercises.Select(e => new ExerciseDto() { Id = e.Id, Name = e.Name });
        }

        public async Task CreateExercise(ExerciseDto exercise, Guid userId)
        {
            var createdByUser = context.Users.Single(u => u.Id == userId);
            await context.Exercises.AddAsync(new DataAccess.Model.Exercise() { Id = exercise.Id, Name = exercise.Name, CreatedByUser = createdByUser });
        }
    }
}
