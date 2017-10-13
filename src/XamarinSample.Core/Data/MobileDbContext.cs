// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using XamarinSample.Core.Entities;

namespace XamarinSample.Core.Data
{
    public class MobileDbContext : DbContext
    {
        private readonly string _dbFileName;

        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<Note> Notes { get; set; }

        public MobileDbContext(string dbFileName)
        {
            _dbFileName = dbFileName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filepath = Path.Combine(dbFolder, _dbFileName);

            optionsBuilder.UseSqlite($"Filename={filepath}");
        }
    }
}
