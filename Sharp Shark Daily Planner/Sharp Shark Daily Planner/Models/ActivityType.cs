namespace Sharp_Shark_Daily_Planner.Models
{
    public class ActivityType : BaseModel
    {
        public string Name { get; set; }
        public int RepeatingDaysInterval { get; set; }
    }
}
