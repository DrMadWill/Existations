using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DrMadWill.Extensions.Cros;

public static class CrosExtension
{
    private static  string _crosName;

    public static IServiceCollection AddCrosServices(this IServiceCollection services,string[] serviceUrls,string crosName =  "AllowSpecificOrigin")
    {
        _crosName = crosName;
        services.AddCors(options =>
        {
            options.AddPolicy(_crosName,
                builder => builder.WithOrigins( (serviceUrls))
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });

        return services;
    }
    
    public static IServiceCollection AddCrosServices(this IServiceCollection services,IConfiguration configuration,string crosName =  "AllowSpecificOrigin",string configurationName = "ServiceUrls")
    {
        _crosName = crosName;
        var serviceUrls =  configuration.GetSection(configurationName).Get<string[]>();
        
        services.AddCors(options =>
        {
            options.AddPolicy(_crosName,
                builder => builder.WithOrigins( (serviceUrls))
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });

        return services;
    } 

    public static IApplicationBuilder UsingCrosServices(IApplicationBuilder app)
    {
        app.UseCors(_crosName);
        return app;
    }



}