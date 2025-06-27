using Microsoft.AspNetCore.Cors.Infrastructure;
using Web.Constants;

namespace Web.Extensions
{
    public static class CorsExtension
    {
        public static void ConfigureCors(this CorsOptions options)
        {
            options.AddPolicy(OwnCorsConstants.CorsPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        }
    }

}
