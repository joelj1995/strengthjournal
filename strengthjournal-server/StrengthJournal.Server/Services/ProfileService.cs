using Microsoft.EntityFrameworkCore;
using StrengthJournal.DataAccess.Contexts;
using StrengthJournal.Server.ApiModels;
using StrengthJournal.Server.Integrations;

namespace StrengthJournal.Server.Services
{
    public class ProfileService
    {
        protected readonly StrengthJournalContext context;
        protected readonly IAuthenticationService authenticationService;

        public ProfileService(StrengthJournalContext context, IAuthenticationService authenticationService)
        {
            this.context = context;
            this.authenticationService = authenticationService;
        }

        public async Task<ProfileSettingsDto> GetSettings(Guid userId)
        {
            var user = await context.Users
                .Include(u => u.PreferredWeightUnit)
                .SingleAsync(u => u.Id == userId);
            return new ProfileSettingsDto()
            {
                PreferredWeightUnit = context.WeightUnits.First(wu => wu.Id.Equals(user.PreferredWeightUnit.Id)).Abbreviation,
                ConsentCEM = user.ConsentCEM
            };
        }

        public async Task UpdateSettings(Guid userId, ProfileSettingsDto settings)
        {
            var user = await context.Users
                .Include(u => u.PreferredWeightUnit)
                .SingleAsync(u => u.Id == userId);
            user.PreferredWeightUnit = context.WeightUnits.First(wu => wu.Abbreviation.Equals(settings.PreferredWeightUnit));
            user.ConsentCEM = settings.ConsentCEM;
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task ResetPassword(Guid userId)
        {
            // TODO: potentially create an audit trail for these and rate limit
            var email = (await context.Users.SingleAsync(u => u.Id.Equals(userId))).Email;
            authenticationService.ResendVerificationEmail(email);
        }
    }
}
