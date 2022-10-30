using StrengthJournal.DataAccess.Contexts;
using StrengthJournal.DataAccess.Model;

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
    }
}
