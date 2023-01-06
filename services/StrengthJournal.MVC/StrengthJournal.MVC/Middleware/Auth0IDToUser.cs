using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StrengthJournal.DataAccess.Contexts;
using System.Threading.Tasks;

namespace StrengthJournal.MVC.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Auth0IDToUser
    {
        private readonly RequestDelegate _next;

        public Auth0IDToUser(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, StrengthJournalContext strengthJournalContext)
        {
            var auth0UserId = httpContext.User.Identity?.Name;
            if (auth0UserId != null)
            {
                var dbUser = await strengthJournalContext.Users.FirstOrDefaultAsync(u => u.ExternalId == auth0UserId);
                Guid? dbUserId = null;
                if (dbUser != null)
                {
                    dbUserId = dbUser.Id;
                }
                else
                {
                    throw new Exception("User ID not registered in the database.");
                }
                httpContext.Items["UserID"] = dbUserId;
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class Auth0IDToUserExtensions
    {
        public static IApplicationBuilder UseAuth0IDToUser(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Auth0IDToUser>();
        }
    }
}
