using AutoMapper;
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
        protected readonly IMapper _mapper;

        public WorkoutService(StrengthJournalContext context, IMapper mapper)
        {
            this.context = context;
            this._mapper = mapper;
        }

        public async Task<WorkoutsPageDto> GetWorkouts(Guid userId, int pageNumber, int perPage)
        {
            var workoutsQuery = context.WorkoutLogEntries
                .Where(wle => wle.User.Id.Equals(userId));
            var workoutsPage = await workoutsQuery
                .OrderByDescending(wle => wle.EntryDateUTC)
                .Skip(perPage * (pageNumber - 1))
                .Take(perPage)
                .ToListAsync();
            return new WorkoutsPageDto()
            {
                PerPage = perPage,
                TotalPages = (workoutsQuery.Count() + perPage - 1) / perPage,
                CurrentPage = pageNumber,
                Workouts = workoutsPage.Select(w => _mapper.Map<WorkoutListDto>(w))
            };
        }

        public async Task<Guid> CreateWorkout(Guid userId, WorkoutCreationUpdateDto workout)
        {
            var user = context.Users.Single(u => u.Id == userId);
            var newWorkout = new WorkoutLogEntry()
            {
                EntryDateUTC = workout.EntryDateUTC,
                Title = workout.Title,
                BodyWeightPIT = workout.Bodyweight,
                BodyWeightPITUnit = context.WeightUnits.FirstOrDefault(wu => wu.Abbreviation.Equals(workout.BodyweightUnit)),
                User = user
            };
            await context.WorkoutLogEntries.AddAsync(newWorkout);
            await context.SaveChangesAsync();
            return newWorkout.Id;
        }

        public async Task<WorkoutDto> GetWorkout(Guid userId, Guid workoutId)
        {
            var workout = await context.WorkoutLogEntries
                .Include(wle => wle.Sets)
                .Include("Sets.Exercise")
                .Include("Sets.WeightUnit")
                .Include("BodyWeightPITUnit")
                .FirstOrDefaultAsync(wle => wle.Id == workoutId && wle.User.Id == userId) ?? throw new EntityNotFoundException();
            return new WorkoutDto()
            {
                Id = workout.Id,
                Title = workout.Title,
                EntryDateUTC = workout.EntryDateUTC,
                Bodyweight = workout.BodyWeightPIT,
                BodyweightUnit = workout.BodyWeightPITUnit?.Abbreviation ?? "",
                Sets = workout.Sets
                    .OrderBy(s => s.Sequence)
                    .Select(set => _mapper.Map<WorkoutSetSync>(set))
            };
        }

        public async Task<IEnumerable<WorkoutSetSync>> GetWorkoutSets(Guid userId, Guid workoutId)
        {
            var user = context.Users.Single(u => u.Id == userId);
            var workout = await context.WorkoutLogEntries
                .Include(wle => wle.Sets)
                .Include("Sets.Exercise")
                .Include("Sets.WeightUnit")
                .FirstOrDefaultAsync(wle => wle.Id == workoutId && wle.User == user);
            if (workout == null)
            {
                throw new EntityNotFoundException();
            }
            return workout.Sets
                .OrderBy(s => s.Sequence)
                .Select(set => _mapper.Map<WorkoutSetSync>(set));
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
                existingSet.Exercise = exercise;
                existingSet.Reps = set.Reps;
                existingSet.TargetReps = set.TargetReps;
                existingSet.RPE = set.RPE;
                existingSet.Weight = set.Weight;
                existingSet.WeightUnit = context.WeightUnits.FirstOrDefault(wu => wu.Abbreviation.Equals(set.WeightUnit));
                context.WorkoutLogEntrySets.Update(existingSet);
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
                    Sequence = maxSeq + 1,
                    Weight = set.Weight,
                    WeightUnit = context.WeightUnits.FirstOrDefault(wu => wu.Abbreviation.Equals(set.WeightUnit))
                };
                await context.WorkoutLogEntrySets.AddAsync(setEntity);
            }
            await context.SaveChangesAsync();
        }

        public async Task DeleteSet(Guid userId, Guid workoutId, Guid setId)
        {
            var user = context.Users.Single(u => u.Id == userId);
            var set = context.WorkoutLogEntrySets.FirstOrDefault(s => s.Id == setId && s.WorkoutLogEntry.Id == workoutId);
            if (set == null)
            {
                throw new EntityNotFoundException();
            }
            context.WorkoutLogEntrySets.Remove(set);
            await context.SaveChangesAsync();
        }

        public async Task UpdateSetSequence(Guid userId, Guid workoutId, WorkoutSetSequenceDto sequenceData)
        {
            var existingWorkout = context.WorkoutLogEntries
                .Include(wle => wle.Sets)
                .FirstOrDefault(wle => wle.User.Id.Equals(userId) && wle.Id.Equals(workoutId)) ?? throw new EntityNotFoundException();
            var workoutSets = existingWorkout.Sets.ToList();
            var newSequence = sequenceData.SetSequence;
            if (workoutSets.Count() != newSequence.Count())
                throw new Exception("Source sequence and target set count do match");
            for (int i = 0; i < workoutSets.Count(); i++)
            {
                var set = workoutSets.First(set => set.Id.Equals(newSequence.ElementAt(i)));
                set.Sequence = i;
                context.WorkoutLogEntrySets.Update(set);
            }
            await context.SaveChangesAsync();
        }

        public async Task UpdateWorkout(Guid userId, Guid workoutId, WorkoutCreationUpdateDto workout)
        {
            var user = context.Users.Single(u => u.Id == userId);
            var existingWorkout = context.WorkoutLogEntries.FirstOrDefault(wle => wle.User.Id.Equals(userId) && wle.Id.Equals(workoutId)) ?? throw new EntityNotFoundException();
            existingWorkout.Title = workout.Title;
            existingWorkout.EntryDateUTC = workout.EntryDateUTC;
            existingWorkout.BodyWeightPIT = workout.Bodyweight;
            existingWorkout.BodyWeightPITUnit = context.WeightUnits.FirstOrDefault(wu => wu.Abbreviation.Equals(workout.BodyweightUnit));
            context.WorkoutLogEntries.Update(existingWorkout);
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
