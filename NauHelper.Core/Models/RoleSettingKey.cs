namespace NauHelper.Core.Models
{
    public class RoleSettingKey
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public string Key { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string DefaultValue { get; set; } = default!;
        public bool IsCommonSetting { get; set; }
    }
}
