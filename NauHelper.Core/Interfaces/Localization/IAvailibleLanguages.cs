using NauHelper.Core.Models;

namespace NauHelper.Core.Interfaces.Localization
{
    public interface IAvailibleLanguages
    {
        public IEnumerable<LanguageInfo> LanguageInfos { get; }
    }
}
