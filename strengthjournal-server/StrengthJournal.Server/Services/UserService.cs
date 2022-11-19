using StrengthJournal.DataAccess.Contexts;

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
    }
}
