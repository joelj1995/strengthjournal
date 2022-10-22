using StrengthJournal.Server.ApiModels;

namespace StrengthJournal.Server.Services
{
    public class ExerciseService
    {
        public async Task<IEnumerable<ExerciseDto>> GetExercises()
        {
            return new List<ExerciseDto>
            {
                new ExerciseDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Squat"
                }
            };
        }
    }
}
