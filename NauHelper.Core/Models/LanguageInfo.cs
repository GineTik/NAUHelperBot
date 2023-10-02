namespace NauHelper.Core.Models
{
    public class LanguageInfo
    {
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;

        public LanguageInfo(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
