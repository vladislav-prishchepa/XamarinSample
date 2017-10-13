// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XamarinSample.Core.Entities
{
    [Table("Tasks")]
    public class TaskEntity
    {
        [Key]
        public long Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public List<Note> Notes { get; set; } = new List<Note>();
    }
}
