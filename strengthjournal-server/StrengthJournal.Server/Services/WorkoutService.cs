using StrengthJournal.DataAccess.Contexts;
using StrengthJournal.DataAccess.Model;
using StrengthJournal.Server.ApiModels;
using StrengthJournal.Server.ServiceExceptions;

namespace StrengthJournal.Server.Services
{
    public class WorkoutService
    {
        protected readonly StrengthJournalContext context;
        public WorkoutService(StrengthJournalContext context)
        {
            this.context = context;
        }

        public async Task<Guid> StartWorkout(Guid userId, DateTime started)
        {
            var user = context.Users.Single(u => u.Id == userId);
            var workout = new WorkoutLogEntry()
            {
                EntryDateUTC = started,
                User = user
            };
            await context.WorkoutLogEntries.AddAsync(workout);
            await context.SaveChangesAsync();
            return workout.Id;
        }

        public async Task SyncWorkoutSet(Guid userId, Guid workoutId, WorkoutSetSync set)
        {
            var user = context.Users.Single(u => u.Id == userId);
            var workout = context.WorkoutLogEntries.FirstOrDefault(wle => wle.Id == workoutId && wle.User == user);
            if (workout == null)
            {
                throw new EntityNotFoundException();
            }
            var exercise = context.Exercises.FirstOrDefault(e => e.Id == set.ExerciseId);
            if (exercise == null)
            {
                throw new EntityNotFoundException();
            }
            
            var existingSet = context.WorkoutLogEntrySets.FirstOrDefault(set => set.Id == workout.Id && set.WorkoutLogEntry == workout);
            if (existingSet != null)
            {
                throw new NotImplementedException();
            }
            else
            {
                var setEntity = new WorkoutLogEntrySet()
                {
                    Id = workout.Id,
                    WorkoutLogEntry = workout,
                    Exercise = exercise,
                    Reps = set.Reps,
                    TargetReps = set.TargetReps,
                    RPE = set.RPE
                };
                if (set.WeightUnit.Equals("kg"))
                {
                    setEntity.WeightKg = set.Weight;
                }
                else
                {
                    setEntity.WeightLbs = set.Weight;
                }
                await context.WorkoutLogEntrySets.AddAsync(setEntity);
            }
            await context.SaveChangesAsync();
        }
    }
}
