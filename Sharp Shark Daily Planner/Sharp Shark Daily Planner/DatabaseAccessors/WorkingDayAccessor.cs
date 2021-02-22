using Sharp_Shark_Daily_Planner.Helpers;
using Sharp_Shark_Daily_Planner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp_Shark_Daily_Planner.DatabaseAccessors
{
    public class WorkingDayAccessor<T> : GenericAccessor<WorkingDay, T>
        where T : PlannerDbContext
    {
        public async Task<WorkingDay> GetCurrentDate()
        {
            using (var context = CreateContext())
            {
                WorkingDay workingDay = context.WorkingDays.Where(w => w.CurrentDay == DateTime.Today).FirstOrDefault();

                if(workingDay == null)
                {
                    workingDay = new WorkingDay() 
                        { 
                            CurrentDay = DateTime.Today
                        };

                    await Insert(workingDay);
                }

                return workingDay;
            }
        }
    }
}
