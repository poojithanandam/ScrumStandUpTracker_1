namespace ScrumStandUpTracker_1.DTOs
{
    public class StatusFormDTO
    {
        public int StatusFormId { get; set; }
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; }
        public string TaskDetails { get; set; }
        public string YesterdayTask { get; set; }   
        public string TodayTask { get; set; }
        public string? Blockers { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}
