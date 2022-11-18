using Microsoft.EntityFrameworkCore;
using StrengthJournal.DataAccess.Contexts;
using StrengthJournal.Server.ApiModels;

namespace StrengthJournal.Server.Services
{
    public class ProfileService
    {
        protected readonly StrengthJournalContext context;

        public ProfileService(StrengthJournalContext context)
        {
            this.context = context;
        }

        public async Task<ProfileSettingsDto> GetSettings(Guid userId)
        {
            var user = await context.Users
                .Include(u => u.PreferredWeightUnit)
                .SingleAsync(u => u.Id == userId);
            return new ProfileSettingsDto()
            {
                PreferredWeightUnit = context.WeightUnits.First(wu => wu.Abbreviation.Equals(user.PreferredWeightUnit.Id)).Abbreviation,
                ConsentCEM = user.ConsentCEM
            };
        }

        public async Task UpdateSettings(Guid userId, ProfileSettingsDto settings)
        {
            var user = await context.Users
                .Include(u => u.PreferredWeightUnit)
                .SingleAsync(u => u.Id == userId);
            user.PreferredWeightUnit = context.WeightUnits.First(wu => wu.Abbreviation.Equals(settings.PreferredWeightUnit));
            settings.ConsentCEM = user.ConsentCEM;
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }
    }
}
