// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Design;
using XamarinSample.Core.Data;

namespace XamarinSample.DatabaseMigration
{
    [UsedImplicitly]
    public class MobileDbContextFactory : IDesignTimeDbContextFactory<MobileDbContext>
    {
        public MobileDbContext CreateDbContext(string[] args) => new MobileDbContext();
    }
}
