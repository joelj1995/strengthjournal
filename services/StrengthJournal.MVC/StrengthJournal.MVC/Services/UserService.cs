using Microsoft.EntityFrameworkCore;
using StrengthJournal.Core.DataAccess.Contexts;
using StrengthJournal.MVC.Integrations;
using StrengthJournal.MVC.Models;

namespace StrengthJournal.MVC.Services
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

        public void RegisterUser(string email, string externalId, bool consentCEM, string countryCode)
        {
            var newUser = new StrengthJournal.Core.DataAccess.Model.User()
            {
                Email = email.ToLower(),
                ExternalId = externalId,
                ConsentCEM = consentCEM,
                UserCountry = context.Countries.Single(c => c.Code.Equals(countryCode)),
                PreferredWeightUnit = context.WeightUnits.Single(wu => wu.Abbreviation == "lbs")
            };
            context.Users.Add(newUser);
            context.SaveChanges();
        }

        public void UpdateEmailAddress(string externalId, string newEmail)
        {
            var user = context.Users.Single(u => u.ExternalId.Equals(externalId));
            user.Email = newEmail;
            context.Users.Update(user);
            context.SaveChanges();
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
                features = featureService.GetFeatures().Result
            };
        }
    }
}
