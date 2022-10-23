using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace StrengthJournal.Server.Extensions
{
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HttpContextExtensions
    {
        public static string? GetUserId(this HttpContext httpContext)
        {
            var item = httpContext.Items["UserID"];
            if (item == null)
            {
                return null;
            }
            return item.ToString();
        }
    }
}
