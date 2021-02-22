using Sharp_Shark_Daily_Planner.DatabaseAccessors;
using Sharp_Shark_Daily_Planner.Helpers;
using Sharp_Shark_Daily_Planner.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Sharp_Shark_Daily_Planner.ViewModels
{
    public class ActivitiesViewModel : BaseViewModel
    {
        public ObservableCollection<Activity> Activities { get; set; }
        public WorkingDay WorkingDay { get; set; }
        private ActivityAccessor<PlannerDbContext> ActivityAccessor { get; set; }
        private WorkingDayAccessor<PlannerDbContext> WorkingDayAccessor { get; set; }

        public ActivitiesViewModel()
        {
            this.Activities = new ObservableCollection<Activity>();
            ActivityAccessor = new ActivityAccessor<PlannerDbContext>();
        }

        public async Task GetAllActivitiesForToday(bool completed)
        {
            WorkingDay = await WorkingDayAccessor.GetCurrentDate();
            var todayActivities = await ActivityAccessor.SelectAllActivititesForToday(completed, WorkingDay);

            foreach (var activity in todayActivities)
            {
                if (!Activities.Contains(activity))
                {
                    Activities.Add(activity);
                }             
            }
        }
    }
}
