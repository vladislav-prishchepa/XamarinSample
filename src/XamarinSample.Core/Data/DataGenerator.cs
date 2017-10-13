// This is an independent project of an individual developer.Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ITCC.Logging.Core;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using XamarinSample.Core.Entities;
using XamarinSample.Core.Utils;

namespace XamarinSample.Core.Data
{
    public static class DataGenerator
    {
        private const string LogScope = "DATA GEN";

        public static async Task<bool> EnsureDataCreatedAsync([NotNull] MobileDbContext dbContext,
            CancellationToken cancellationToken)
            => (await DbOperationsWrappers.PerformSafelyAsync(dbContext, async db =>
            {
                await db.Database.MigrateAsync(cancellationToken);

                Logger.LogDebug(LogScope, "Migration performed.");

                if (await db.Tasks.AnyAsync(cancellationToken))
                {
                    Logger.LogDebug(LogScope, "Data already created.");
                    return new ErrorOr<Nothing>(new Nothing());
                }

                var rnd = new Random();

                var tasks = new List<TaskEntity>();

                for (var i = 1; i <= 10; i++)
                {
                    var task = new TaskEntity
                    {
                        Title = $"Task {i}",
                        Description = $"Task {i} description. {Guid.NewGuid().ToString()}",
                        Start = new DateTime(2017,10,rnd.Next(1,10)),
                        End = new DateTime(2017,10,rnd.Next(11,30))
                    };

                    if (rnd.NextDouble() > 0.6)
                    {
                        for (var j = 0; j < rnd.Next(1,3); j++)
                        {
                            task.Notes.Add(new Note
                            {
                                Created = DateTime.Now,
                                Text = $"{task.Title} note {j} : {Guid.NewGuid()}"
                            });
                        }
                    }

                    tasks.Add(task);
                }

                db.Tasks.AddRange(tasks);

                await db.SaveChangesAsync(cancellationToken);

                Logger.LogDebug(LogScope, "Tasks saved.");

                return new ErrorOr<Nothing>(new Nothing());

            })).IsSuccess;
    }
}
