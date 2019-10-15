using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DiagnosticPlatform.Mapping.UI.helpers
{
    public static class LocalizedSpaStaticFilesExtensions
    {
        public static void AddLocalizedSpaStaticFiles(this IServiceCollection services,
                                                      string[] availableLocales,
                                                      string spaRootPath)
        {
            services.AddTransient<IUserLanguageService>(serviceProvider =>
                {
                    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                    return   new  UserLanguageService(httpContextAccessor, availableLocales);
                });
            services.AddTransient<LocalizedSpaStaticFilePathProvider>(serviceProvider =>
                {
                    var userLanguageService = serviceProvider.GetRequiredService<IUserLanguageService>();
                    return new LocalizedSpaStaticFilePathProvider(userLanguageService, spaRootPath);
                });
        }

        public static void UseLocalizedSpaStaticFiles(this IApplicationBuilder applicationBuilder, string defaultFile)
        {
            applicationBuilder.Use((context, next) =>
                {
                    // In this part of the pipeline, the request path is altered to point to a localized SPA asset
                    var spaFilePathProvider = context.RequestServices.GetRequiredService<LocalizedSpaStaticFilePathProvider>();
                    context.Request.Path = spaFilePathProvider.GetRequestPath("/" + defaultFile.TrimStart('/'));
                    return next();
                });

            applicationBuilder.UseStaticFiles();
        }
    }
}
