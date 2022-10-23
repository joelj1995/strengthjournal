using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StrengthJournal.DataAccess.Contexts;
using System.Threading.Tasks;

namespace StrengthJournal.Server.Middleware
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
                var dbUserId = await strengthJournalContext.Users.FirstOrDefaultAsync(u => u.Handle == auth0UserId);
                if (dbUserId != null)
                {
                    httpContext.Items["UserID"] = dbUserId.Id;
                }
                else
                {
                    var newUser = new StrengthJournal.DataAccess.Model.User()
                    {
                        Handle = auth0UserId,
                    };
                    strengthJournalContext.Users.Add(newUser);
                    await strengthJournalContext.SaveChangesAsync();
                    httpContext.Items["UserID"] = newUser.Id;
                }
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
