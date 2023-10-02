namespace NauHelper.Core.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public int RoleSettingKeyId { get; set; }
        public string Value { get; set; } = default!;
    }
}
