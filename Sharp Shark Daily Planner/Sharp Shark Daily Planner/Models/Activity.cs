using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sharp_Shark_Daily_Planner.Models
{
    public class Activity : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [ForeignKey("ActivityType")]
        public int ActivityTypeId { get; set; }
        public ActivityType ActivityType { get; set; }
        public bool Completed { get; set; }
        [ForeignKey("WorkingDay")]
        public int WorkingDayId { get; set; }
        public WorkingDay WorkingDay { get; set; }
    }
}
