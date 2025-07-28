using System.ComponentModel.DataAnnotations;

namespace ScrumStandUpTracker_1.Models
{
    public class Developer
    {
        [Key]
        public int DeveloperId { get; set; }
        [Required, MaxLength(100)]
        public string Username { get; set; }
        [Required] 
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public ICollection<StatusForm> Statusform { get; set; } = new List<StatusForm>();
    }
}
