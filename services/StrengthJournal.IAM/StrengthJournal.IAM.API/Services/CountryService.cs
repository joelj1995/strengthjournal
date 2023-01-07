using Microsoft.EntityFrameworkCore;
using StrengthJournal.Core.DataAccess.Contexts;
using StrengthJournal.IAM.API.Models;

namespace StrengthJournal.IAM.API.Services
{
    public class CountryService
    {
        protected readonly StrengthJournalContext context;

        public CountryService(StrengthJournalContext context) 
        { 
            this.context = context;
        }

        public async Task<ICollection<CountryResponse>> GetCountries()
        {
            return await context.Countries
                .Select(c => new CountryResponse { Code = c.Code, Name = c.Name })
                .ToListAsync();
        }
    }
}
