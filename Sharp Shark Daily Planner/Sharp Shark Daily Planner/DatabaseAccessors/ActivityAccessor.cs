using Microsoft.EntityFrameworkCore;
using Sharp_Shark_Daily_Planner.Helpers;
using Sharp_Shark_Daily_Planner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharp_Shark_Daily_Planner.DatabaseAccessors
{
    public class ActivityAccessor<T> : GenericAccessor<Activity, T>
        where T : PlannerDbContext
    {
        public async Task<List<Activity>> SelectAllActivititesForToday(bool completed, WorkingDay workingDay)
        {
            using (var context = CreateContext())
            {
                List<Activity> activities = new List<Activity>();

                await Task.Run(() =>
                {
                    AppendDailyActivities(context, activities, workingDay, completed);
                    AppendWeeklyActivities(context, activities, workingDay, completed, 7);
                    //if desired, extend with months and years...
                });
                
                return activities;
            }
        } 

        private async void AppendDailyActivities(PlannerDbContext context, List<Activity> activities, WorkingDay workingDay, bool completed)
        {
            List<Activity> dailyActivities = await context.Activities
                .Where(a => a.ActivityType.RepeatingDaysInterval == 1
                    && (a.EndDate == null || a.EndDate < DateTime.Today)
                    && a.Completed == completed
                    && a.WorkingDay == workingDay
                ).ToListAsync();

            activities.AddRange(dailyActivities);
        }

        private async void AppendWeeklyActivities(PlannerDbContext context, List<Activity> activities, WorkingDay workingDay, bool completed, int daysSpan)
        {
            List<Activity> dailyActivities = await context.Activities
                .Where(a => a.ActivityType.RepeatingDaysInterval == daysSpan 
                    && DateTime.Today.Subtract(a.StartDate).Days % daysSpan == 0
                    && (a.EndDate == null || a.EndDate < DateTime.Today)
                    && a.Completed == completed
                    && a.WorkingDay == workingDay
                ).ToListAsync();

            activities.AddRange(dailyActivities);
        }
    }
}
