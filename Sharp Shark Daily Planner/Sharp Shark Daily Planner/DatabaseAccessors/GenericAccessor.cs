using Microsoft.EntityFrameworkCore;
using Sharp_Shark_Daily_Planner.Helpers;
using Sharp_Shark_Daily_Planner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharp_Shark_Daily_Planner.DatabaseAccessors
{
    public class GenericAccessor<T, U>
        where T: BaseModel
        where U: PlannerDbContext
    {
        protected PlannerDbContext CreateContext()
        {
            PlannerDbContext context = (U)Activator.CreateInstance(typeof(U));
            context.Database.EnsureCreated();
            context.Database.Migrate();

            return context;
        }

        public async Task<T> SelectById(int id)
        {
            using (var context = CreateContext())
            {
                return await context.Set<T>().FindAsync(id);
            }
        }

        public async Task<IReadOnlyList<T>> SelectAllAsReadOnly()
        {
            using (var context = CreateContext())
            {
                return await context.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<T>> SelectAllAsReadWriteAsync()
        {
            using (var context = CreateContext())
            {
                return await context.Set<T>().ToListAsync();
            }
        }

        public async Task Insert(T entity)
        {
            using (var context = CreateContext())
            {
                bool newEntity = context.Set<T>().Any(e => e.Equals(entity));

                if (newEntity)
                {
                    await context.Set<T>().AddAsync(entity);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task Update(T entity)
        {
            using (var context = CreateContext())
            {
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(T entity)
        {
            using (var context = CreateContext())
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
