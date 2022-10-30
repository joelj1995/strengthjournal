using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrengthJournal.Server.ApiModels;
using StrengthJournal.Server.Extensions;
using StrengthJournal.Server.Services;

namespace StrengthJournal.Server.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly WorkoutService workoutService;

        public WorkoutsController(WorkoutService workoutService)
        {
            this.workoutService = workoutService;
        }

        [HttpPost]
        public async Task<Guid> CreateWorkout([FromBody]WorkoutCreationDto workout)
        {
            var userId = HttpContext.GetUserId();
            var workoutId = await workoutService.StartWorkout(userId, workout.StartDate);
            return workoutId;
        }
    }
}
