using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiagnosticPlatform.Mapping.UI.helpers
{
    public class LocalizedSpaStaticFilePathProvider
    {
        private readonly IUserLanguageService _userLanguageService;
        private readonly string _distFolder;

        public LocalizedSpaStaticFilePathProvider(IUserLanguageService userLanguageService,
                                                  string distFolder)
        {
            _userLanguageService = userLanguageService;
            _distFolder = distFolder;
        }

        public string GetRequestPath(string subpath)
        {
            var userLocale = _userLanguageService.GetUserLocale();
            var spaFilePath = "/" + _distFolder + "/" + userLocale + subpath;
            return spaFilePath;
        }
    }
}
