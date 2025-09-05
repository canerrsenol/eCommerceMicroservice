using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.SharedLibrary.DependencyInjection
{
    public static class JWTAuthenticationSceme
    {
        public static IServiceCollection AddJWTAuthenticationSceme(this IServiceCollection services
            ,IConfiguration config)
        {
            // Add JWT Authentication Scheme
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Bearer", options =>
                {
                   
                }); 
        }
    }
}
