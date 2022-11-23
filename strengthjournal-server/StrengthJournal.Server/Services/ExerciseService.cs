using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StrengthJournal.DataAccess.Contexts;
using StrengthJournal.Server.ApiModels;
using StrengthJournal.Server.ServiceExceptions;

namespace StrengthJournal.Server.Services
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

        public async Task<DataPage<ExerciseDto>> GetExercises(Guid userId, int pageNumber, int perPage, bool allRecords = false)
        {
            var user = context.Users.Single(u => u.Id == userId);
            var exercisesQuery = context.Exercises
                .Where(e => e.CreatedByUser == null || e.CreatedByUser == user)
                .OrderBy(e => e.Name);
            var totalRecords = await exercisesQuery.CountAsync();
            if (!allRecords) exercisesQuery = (IOrderedQueryable<DataAccess.Model.Exercise>)exercisesQuery.Skip(perPage * (pageNumber - 1)).Take(perPage);
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

        public async Task<IEnumerable<ExerciseHistoryDto>> GetExerciseHistory(Guid userId, Guid exerciseId, int pageSize = 10)
        {
            var history = await context.ExerciseHistory
                .Where(line => line.ExerciseId.Equals(exerciseId) && line.UserId.Equals(userId))
                .OrderByDescending(line => line.EntryDateUTC)
                .Take(pageSize)
                .Select(line => _mapper.Map<ExerciseHistoryDto>(line))
                .ToListAsync();
            return history;
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
            await context.Exercises.AddAsync(new DataAccess.Model.Exercise() { Name = name, CreatedByUser = createdByUser, ParentExerciseId = parentExerciseId });
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
