namespace NauHelper.Core.Localization
{
    public interface ILocalizer
    {
        string Get(string key);
        string Get(string key, string lang);
    }
}
