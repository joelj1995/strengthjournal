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

        public async Task<IEnumerable<ExerciseHistoryDto>> GetExerciseHistory(Guid userId, Guid exerciseId, int pageSize = 10)
        {
            return context.WorkoutLogEntrySets
                .Include("WeightUnit")
                .Include("WorkoutLogEntry")
                .Where(set => set.Exercise.Id.Equals(exerciseId) && set.WorkoutLogEntry.User.Id.Equals(userId))
                .OrderByDescending(set => set.WorkoutLogEntry.EntryDateUTC)
                .Take(pageSize)
                .Select(set => new ExerciseHistoryDto()
                {
                    EntryDateUTC = set.WorkoutLogEntry.EntryDateUTC,
                    Weight = set.Weight,
                    WeightUnit = set.WeightUnit == null ? "" : set.WeightUnit.Abbreviation,
                    Reps = set.Reps,
                    TargetReps = set.TargetReps,
                    RPE = set.RPE
                });
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
