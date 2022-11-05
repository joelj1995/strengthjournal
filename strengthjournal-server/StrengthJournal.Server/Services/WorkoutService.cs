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

        public async Task<IEnumerable<WorkoutListDto>> GetWorkouts(Guid userId)
        {
            return await context.WorkoutLogEntries.Where(wle => wle.User.Id.Equals(userId)).OrderBy(wle => wle.EntryDateUTC).Select(wle => new WorkoutListDto()
            {
                Id = wle.Id,
                Title = wle.Title,
                EntryDateUTC = wle.EntryDateUTC
            }).ToListAsync();
        }

        public async Task<Guid> CreateWorkout(Guid userId, WorkoutCreationDto workout)
        {
            var user = context.Users.Single(u => u.Id == userId);
            var newWorkout = new WorkoutLogEntry()
            {
                EntryDateUTC = workout.EntryDateUTC,
                Title = workout.Title,
                User = user
            };
            await context.WorkoutLogEntries.AddAsync(newWorkout);
            await context.SaveChangesAsync();
            return newWorkout.Id;
        }

        public async Task<IEnumerable<WorkoutSetSync>> GetWorkoutSets(Guid userId, Guid workoutId)
        {
            var user = context.Users.Single(u => u.Id == userId);
            var workout = await context.WorkoutLogEntries.Include(wle => wle.Sets).Include("Sets.Exercise").FirstOrDefaultAsync(wle => wle.Id == workoutId && wle.User == user);
            if (workout == null)
            {
                throw new EntityNotFoundException();
            }
            // TODO: implement automapper to make this less painful
            return workout.Sets.OrderBy(s => s.Sequence).Select(set => new WorkoutSetSync { Id = set.Id, ExerciseId = set.Exercise.Id, ExerciseName = set.Exercise.Name, Reps = set.Reps, TargetReps = set.TargetReps, Weight = set.WeightLbs == null ? set.WeightKg : set.WeightLbs, WeightUnit = set.WeightLbs == null ? "kg" : "lbs", RPE = set.RPE });
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
                var lastSet = context.WorkoutLogEntrySets.Where(s => s.WorkoutLogEntry.Id == workoutId);
                var maxSeq = lastSet.Count() > 0 ? lastSet.Max(s => s.Sequence) : 0;
                var setEntity = new WorkoutLogEntrySet()
                {
                    Id = set.Id,
                    WorkoutLogEntry = workout,
                    Exercise = exercise,
                    Reps = set.Reps,
                    TargetReps = set.TargetReps,
                    RPE = set.RPE,
                    Sequence = maxSeq + 1
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

        public async Task DeleteWorkout(Guid userId, Guid workoutId)
        {
            var user = context.Users.Single(u => u.Id == userId);
            var workout = context.WorkoutLogEntries.FirstOrDefault(wle => wle.Id == workoutId && wle.User == user);
            if (workout == null)
            {
                throw new EntityNotFoundException();
            }
            context.Remove(workout);
            await context.SaveChangesAsync();
        }
    }
}
