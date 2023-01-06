using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StrengthJournal.Core.DataAccess.Contexts;
using StrengthJournal.Journal.API.ApiModels;
using StrengthJournal.Core;
using StrengthJournal.Journal.API.ServiceExceptions;

namespace StrengthJournal.Journal.API.Services
{
    public class ExerciseService
    {
        protected readonly StrengthJournalContext context;
        protected readonly IMapper _mapper;

        public ExerciseService(StrengthJournalContext context, IMapper mapper)
        {
            this.context = context;
            this._mapper = mapper;
        }

        public async Task<ExerciseDto> GetExercise(Guid userId, Guid exerciseId)
        {
            var exercise = await context.Exercises
                .SingleOrDefaultAsync(e => e.Id.Equals(exerciseId) && (e.CreatedByUser == null || e.CreatedByUser.Id.Equals(userId)));
            if (exercise == null)
                throw new EntityNotFoundException();
            return new ExerciseDto()
            {
                Id = exerciseId,
                Name = exercise.Name,
                SystemDefined = exercise.CreatedByUser == null,
                ParentExerciseId = exercise.ParentExerciseId
            };
        }

        public async Task<DataPage<ExerciseDto>> GetExercises(Guid userId, int pageNumber, int perPage, string? search, bool allRecords = false)
        {
            var user = context.Users.Single(u => u.Id == userId);
            var exercisesQuery = context.Exercises
                .Where(e => e.CreatedByUser == null || e.CreatedByUser == user)
                .OrderBy(e => e.Name);
            var totalRecords = await exercisesQuery.CountAsync();
            if (search != null) exercisesQuery = (IOrderedQueryable<StrengthJournal.Core.DataAccess.Model.Exercise>)exercisesQuery.Where(e => e.Name.ToLower().StartsWith(search.ToLower()));
            if (!allRecords) exercisesQuery = (IOrderedQueryable<StrengthJournal.Core.DataAccess.Model.Exercise>)exercisesQuery.Skip(perPage * (pageNumber - 1)).Take(perPage);
            var data = await exercisesQuery
                .Select(e => new ExerciseDto() { Id = e.Id, Name = e.Name, SystemDefined = e.CreatedByUser == null, ParentExerciseId = e.ParentExerciseId })
                .ToListAsync();
            return new DataPage<ExerciseDto>()
            {
                PerPage = perPage,
                TotalRecords = totalRecords,
                CurrentPage = pageNumber,
                Data = data
            };
        }

        public async Task<DataPage<ExerciseHistoryDto>> GetExerciseHistory(Guid userId, Guid exerciseId, int pageNumber, int perPage, Guid? excludeWorkoutId = null)
        {
            var historyQuery = context.ExerciseHistory
                .Where(line => line.ExerciseId.Equals(exerciseId) && line.UserId.Equals(userId));
            if (excludeWorkoutId != null)
                historyQuery = historyQuery.Where(l => l.WorkoutId != excludeWorkoutId);
            var totalRecords = await historyQuery.CountAsync();
            var data = await historyQuery
                .OrderByDescending(line => line.EntryDateUTC)
                .Skip(perPage * (pageNumber - 1))
                .Take(perPage)
                .Select(line => _mapper.Map<ExerciseHistoryDto>(line))
                .ToListAsync();
            return new DataPage<ExerciseHistoryDto>()
            {
                PerPage = perPage,
                TotalRecords = totalRecords,
                CurrentPage = pageNumber,
                Data = data
            };
        }

        public async Task CreateExercise(string name, Guid userId, Guid? parentExerciseId)
        {
            var createdByUser = context.Users.Single(u => u.Id == userId);
            if (parentExerciseId != null)
            {
                var parentExercsie = context.Exercises.Single(e => e.Id.Equals(parentExerciseId));
                if (parentExercsie.ParentExerciseId != null)
                    throw new Exception("Cannot add non-root exercise as parent");
            }
            await context.Exercises.AddAsync(new StrengthJournal.Core.DataAccess.Model.Exercise() { Name = name, CreatedByUser = createdByUser, ParentExerciseId = parentExerciseId });
            await context.SaveChangesAsync();
        }

        public async Task UpdateExercise(Guid userId, Guid exerciseId, string name, Guid? parentExerciseId)
        {
            var exercise = context.Exercises.FirstOrDefault(exercise => exercise.Id.Equals(exerciseId) && exercise.CreatedByUser.Id.Equals(userId));
            if (exercise == null)
            {
                throw new EntityNotFoundException();
            }
            if (parentExerciseId != null)
            {
                var parentExercsie = context.Exercises.Single(e => e.Id.Equals(parentExerciseId));
                if (parentExercsie.ParentExerciseId != null)
                    throw new Exception("Cannot add non-root exercise as parent");
            }
            exercise.Name = name;
            exercise.ParentExerciseId = parentExerciseId;
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
