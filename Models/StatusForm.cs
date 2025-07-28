using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumStandUpTracker_1.Models
{
    public class StatusForm
    {
        [Key]
        public int StatusFormId { get; set; }
        [Required]
        public int DeveloperId { get; set; }
        [ForeignKey("DeveloperId")]
        public Developer? developer { get; set; }
        [Required]
        public string DeveloperName { get; set; }
        [Required, MaxLength(200)] 
        [Column("Task Details")]
        public string TaskDetails { get; set; }
        [Required, MaxLength(500)]
        [Column("Yesterday Task")]
        public string YesterdayTask { get; set; }
        [Required, MaxLength(500)]
        [Column("Today Task")]
        public string TodayTask { get; set; }
        [MaxLength(500)]
        public string? Blockers { get; set; } 
        [Required]
        public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;

    }
}
