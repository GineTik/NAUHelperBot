using NauHelper.Core.Interfaces.Localization;
using NauHelper.Core.Models;

namespace NauHelper.Infrastructure.Localization
{
    public class AvailibleLanguages : IAvailibleLanguages
    {
        public IEnumerable<LanguageInfo> LanguageInfos => new[]
        {
            new LanguageInfo("Українська", "ua"),
            new LanguageInfo("English", "en-US")
       };
    }
}
