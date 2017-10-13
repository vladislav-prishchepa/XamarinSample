// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using Microsoft.EntityFrameworkCore;
using XamarinSample.Core.Entities;

namespace XamarinSample.Core.Data
{
    public class MobileDbContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<Note> Notes { get; set; }

        public MobileDbContext()
        {
            _connectionString = "Filename=database.db";
        }
        public MobileDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
