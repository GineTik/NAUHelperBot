using System.ComponentModel.DataAnnotations.Schema;

namespace NauHelper.Core.Entities
{
    public class Specialty
    {
        // code
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public int FacultyId { get; set; }
    }
}
