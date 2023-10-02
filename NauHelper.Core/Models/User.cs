using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NauHelper.Core.Models
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
