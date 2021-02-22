using System.ComponentModel.DataAnnotations;

namespace Sharp_Shark_Daily_Planner.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
