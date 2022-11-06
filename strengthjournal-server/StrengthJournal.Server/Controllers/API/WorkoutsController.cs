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

        [HttpGet("{workoutId:Guid}")]
        public async Task<ActionResult<WorkoutDto>> GetWorkout(Guid workoutId)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                var workout = await workoutService.GetWorkout(userId, workoutId);
                return Ok(workout);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<Guid> CreateWorkout(WorkoutCreationUpdateDto workout)
        {
            var userId = HttpContext.GetUserId();
            var workoutId = await workoutService.CreateWorkout(userId, workout);
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

        [HttpDelete("{workoutId:Guid}/sets/{setId:Guid}")]
        public async Task<ActionResult> DeleteSet([FromRoute] Guid workoutId, [FromRoute] Guid setId)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                await workoutService.DeleteSet(userId, workoutId, setId);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut("{workoutId:Guid}")]
        public async Task<ActionResult> UpdateWorkout([FromRoute] Guid workoutId, WorkoutCreationUpdateDto workout)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                await workoutService.UpdateWorkout(userId, workoutId, workout);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{workoutId:Guid}")]
        public async Task<ActionResult> DeleteWorkout([FromRoute] Guid workoutId)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                await workoutService.DeleteWorkout(userId, workoutId);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
