using Microsoft.EntityFrameworkCore;
using StrengthJournal.DataAccess.Contexts;
using StrengthJournal.Server.Models;

namespace StrengthJournal.Server.Services
{
    public class UserService
    {
        protected readonly StrengthJournalContext context;

        public UserService(StrengthJournalContext context)
        {
            this.context = context;
        }

        public void RegisterUser(string email, string externalId, bool consentCEM, string countryCode)
        {
            var newUser = new StrengthJournal.DataAccess.Model.User()
            {
                Email = email,
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
            var config = new AppConfig()
            {
                PrefferedWeightUnit = user.PreferredWeightUnit?.Abbreviation ?? "lbs"
            };
            return config;
        }
    }
}
