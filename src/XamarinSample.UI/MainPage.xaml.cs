// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using XamarinSample.Core.Data;
using XamarinSample.Core.Entities;
using XamarinSample.Core.Utils;

namespace XamarinSample.UI
{
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void MainPage_OnAppearing(object sender, EventArgs e)
        {
            var dbFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var fileName = "Test.db";
            var dbFullPath = Path.Combine(dbFolder, fileName);

            using (var dbContext = new MobileDbContext($"Filename={dbFullPath}"))
            {
                var migrateResult = await DbOperationsWrappers.PerformSafelyAsync(dbContext, async db =>
                {
                    await db.Database.MigrateAsync();
                    return new ErrorOr<Nothing>(new Nothing());
                });

                if (migrateResult.IsError)
                {
                    Debug.WriteLine(migrateResult.Error.Exception.Message);
                    return;
                }

                Debug.WriteLine("DbMigrated");

                var createTasksResult = await DbOperationsWrappers.PerformSafelyAsync(dbContext, async db =>
                {
                    var tasks = new List<TaskEntity>();
                    var startBase = new DateTime(2017, 10, 1);
                    for (var i = 1; i <= 100; i++)
                    {
                        tasks.Add(new TaskEntity
                        {
                            Title = $"Task {i}",
                            Description = Guid.NewGuid().ToString(),
                            Start = startBase.AddDays(i),
                            End = startBase.AddDays(i+2)
                        });
                    }

                    db.Tasks.AddRange(tasks);
                    await db.SaveChangesAsync();

                    return new ErrorOr<Nothing>(new Nothing());
                });

                if (createTasksResult.IsError)
                {
                    Debug.WriteLine(createTasksResult.Error.Exception.Message);
                    return;
                }

                Debug.WriteLine("Tasks created");

                var loadTasksResult = await DbOperationsWrappers.PerformSafelyAsync(dbContext, async db =>
                {
                    var tasks = await db.Tasks.AsNoTracking().ToListAsync();
                    return new ErrorOr<List<TaskEntity>>(tasks);
                });


                if (loadTasksResult.IsError)
                {
                    Debug.WriteLine(loadTasksResult.Error.Exception.Message);
                    return;
                }

                Debug.WriteLine("Tasks loaded");
            }
        }
    }
}
