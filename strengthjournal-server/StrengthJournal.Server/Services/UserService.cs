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

        public void RegisterUser(string email, string externalId, bool consentCEM)
        {
            var newUser = new StrengthJournal.DataAccess.Model.User()
            {
                Email = email,
                ExternalId = externalId,
                ConsentCEM = consentCEM
            };
            context.Users.Add(newUser);
            context.SaveChanges();
        }
    }
}
