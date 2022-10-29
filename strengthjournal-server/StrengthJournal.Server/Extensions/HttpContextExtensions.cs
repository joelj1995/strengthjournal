using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace StrengthJournal.Server.Extensions
{
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HttpContextExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            var item = httpContext.Items["UserID"];
            if (item == null)
            {
                throw new AuthenticationException("No UserID was found in the HTTP context items");
            }
            Guid userId = Guid.Empty; 
            if (!Guid.TryParse(item.ToString(), out userId))
            {
                throw new AuthenticationException($"Could not parse UserID '{item.ToString()}' as a Guid");
            }
            return userId;
        }
    }
}
