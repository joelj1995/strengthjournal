using Microsoft.EntityFrameworkCore;
using StrengthJournal.DataAccess.Contexts;
using StrengthJournal.Server.ApiModels;
using StrengthJournal.Server.ServiceExceptions;

namespace StrengthJournal.Server.Services
{
    public class ExerciseService
    {
        protected readonly StrengthJournalContext context;

        public ExerciseService(StrengthJournalContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ExerciseDto>> GetExercises(Guid userId)
        {
            var user = context.Users.Single(u => u.Id == userId);
            var exercises = await context.Exercises.Where(e => e.CreatedByUser == null || e.CreatedByUser == user).ToListAsync();
            return exercises.Select(e => new ExerciseDto() { Id = e.Id, Name = e.Name, SystemDefined = e.CreatedByUser == null });
        }

        public async Task CreateExercise(string name, Guid userId)
        {
            var createdByUser = context.Users.Single(u => u.Id == userId);
            await context.Exercises.AddAsync(new DataAccess.Model.Exercise() { Name = name, CreatedByUser = createdByUser });
            await context.SaveChangesAsync();
        }

        public async Task UpdateExercise(Guid userId, Guid exerciseId, string name)
        {
            var exercise = context.Exercises.FirstOrDefault(exercise => exercise.Id.Equals(exerciseId) && exercise.CreatedByUser.Id.Equals(userId));
            if (exercise == null)
            {
                throw new EntityNotFoundException();
            }
            exercise.Name = name;
            context.Exercises.Update(exercise);
            await context.SaveChangesAsync();
        }

        public async Task DeleteExercise(Guid userId, Guid exerciseId)
        {
            var exercise = context.Exercises.FirstOrDefault(exercise => exercise.Id.Equals(exerciseId) && exercise.CreatedByUser.Id.Equals(userId));
            if (exercise == null)
            {
                throw new EntityNotFoundException();
            }
            context.Exercises.Remove(exercise);
            await context.SaveChangesAsync();
        }
    }
}
