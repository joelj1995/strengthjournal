using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<WorkoutSetSync>> GetWorkoutSets(Guid userId, Guid workoutId)
        {
            var user = context.Users.Single(u => u.Id == userId);
            var workout = context.WorkoutLogEntries.Include(wle => wle.Sets).Include("Sets.Exercise").FirstOrDefault(wle => wle.Id == workoutId && wle.User == user);
            if (workout == null)
            {
                throw new EntityNotFoundException();
            }
            // TODO: implement automapper to make this less painful
            return workout.Sets.Select(set => new WorkoutSetSync { Id = set.Id, ExerciseId = set.Exercise.Id, ExerciseName = set.Exercise.Name, Reps = set.Reps, TargetReps = set.TargetReps, Weight = set.WeightLbs == null ? set.WeightKg : set.WeightLbs, WeightUnit = set.WeightLbs == null ? "kg" : "lbs", RPE = set.RPE });
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
            var existingSet = context.WorkoutLogEntrySets.FirstOrDefault(s => s.Id == set.Id && s.WorkoutLogEntry == workout);
            if (existingSet != null)
            {
                throw new NotImplementedException();
            }
            else
            {
                // TODO: implement automapper to make this less painful
                var setEntity = new WorkoutLogEntrySet()
                {
                    Id = set.Id,
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
