using NauHelper.Core.Entities;

namespace NauHelper.Core.Models
{
    public class StudentInfo
    {
        public Faculty Faculty { get; set; } = default!;
        public Specialty Specialty { get; set; } = default!;
        public Group Group { get; set; } = default!;
    }
}
