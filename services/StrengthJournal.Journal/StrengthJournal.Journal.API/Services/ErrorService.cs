﻿using StrengthJournal.Core.DataAccess.Contexts;
using StrengthJournal.Core.DataAccess.Model;

namespace StrengthJournal.Journal.API.Services
{
    public class ErrorService
    {
        protected readonly StrengthJournalContext context;

        public ErrorService(StrengthJournalContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task LogError(Guid id, Guid userId, string errorMessage, Guid? apiTraceId = null)
        {
            var newError = new AppError()
            {
                Id = id,
                UserId = userId,
                ApiErrorTraceId = apiTraceId,
                DateCreated = DateTime.Now,
                Message = errorMessage
            };
            await context.AppErrors.AddAsync(newError);
            await context.SaveChangesAsync();
        }
    }
}
