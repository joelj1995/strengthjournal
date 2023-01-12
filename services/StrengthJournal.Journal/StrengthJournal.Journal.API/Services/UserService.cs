using Microsoft.EntityFrameworkCore;
using StrengthJournal.Core.DataAccess.Contexts;
using StrengthJournal.Core.Integrations;
using StrengthJournal.Core;
using StrengthJournal.Core.ApiModels;

namespace StrengthJournal.Journal.API.Services
{
    public class UserService
    {
        protected readonly StrengthJournalContext context;
        private readonly IFeatureService featureService;

        public UserService(StrengthJournalContext context, IFeatureService featureService)
        {
            this.context = context;
            this.featureService = featureService;
        }

        public AppConfig GetConfig(string email)
        {
            var user = context.Users
                .Include(u => u.PreferredWeightUnit)
                .Single(u => u.Email.Equals(email));
            return GetConfig(user);
        }

        public AppConfig GetConfig(Guid userId)
        {
            var user = context.Users
                .Include(u => u.PreferredWeightUnit)
                .Single(u => u.Id.Equals(userId));
            return GetConfig(user);
        }

        private AppConfig GetConfig(StrengthJournal.Core.DataAccess.Model.User user)
        {
            return new AppConfig()
            {
                preferredWeightUnit = user.PreferredWeightUnit?.Abbreviation ?? "lbs",
                features = featureService.GetFeatures().Result,
                userName = user.Email
            };
        }
    }
}
