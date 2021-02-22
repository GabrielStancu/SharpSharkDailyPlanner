﻿using Microsoft.EntityFrameworkCore;
using Sharp_Shark_Daily_Planner.Models;
using System;
using System.IO;
using Xamarin.Forms;

namespace Sharp_Shark_Daily_Planner.Helpers
{
    public class PlannerDbContext : DbContext
    {
        public DbSet<Activity> Activities { get; private set; }
        public DbSet<ActivityType> ActivityTypes { get; private set; }
        public DbSet<WorkingDay> WorkingDays { get; private set; }

        private const string _databaseName = "planner.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databasePath;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    SQLitePCL.Batteries_V2.Init();
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", _databaseName);
                    break;
                case Device.Android:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), _databaseName);
                    break;
                default:
                    throw new NotImplementedException("Platform not supported.");
            }

            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
    }
}
