using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrengthJournal.Server.ApiModels;
using StrengthJournal.Server.Extensions;
using StrengthJournal.Server.ServiceExceptions;
using StrengthJournal.Server.Services;

namespace StrengthJournal.Server.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly WorkoutService workoutService;

        public WorkoutsController(WorkoutService workoutService)
        {
            this.workoutService = workoutService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutListDto>>> GetWorkouts()
        {
            // TODO: paginate this endpoint
            var userId = HttpContext.GetUserId();
            var workouts = await workoutService.GetWorkouts(userId);
            return Ok(workouts);
        }

        [HttpPost]
        public async Task<Guid> CreateWorkout([FromBody]WorkoutCreationDto workout)
        {
            var userId = HttpContext.GetUserId();
            var workoutId = await workoutService.StartWorkout(userId, workout.StartDate);
            return workoutId;
        }

        [HttpGet("{workoutId:Guid}/sets")]
        public async Task<ActionResult> GetSets([FromRoute]Guid workoutId)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                var sets = await workoutService.GetWorkoutSets(userId, workoutId);
                return Ok(sets);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound();
            }
        }


        [HttpPut("{workoutId:Guid}/sets")]
        public async Task<ActionResult> SyncSet([FromRoute] Guid workoutId, WorkoutSetSync set)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                await workoutService.SyncWorkoutSet(userId, workoutId, set);
            }
            catch(EntityNotFoundException e)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
