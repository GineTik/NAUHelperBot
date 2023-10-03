namespace NauHelper.Core.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public int SpecialtyId { get; set; }
    }
}
