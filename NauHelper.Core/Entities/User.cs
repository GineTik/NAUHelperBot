using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NauHelper.Core.Entities
{
    public class User
    {
        /// <summary>
        /// TelegramUserId
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TelegramId { get; set; }
    }
}
