// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XamarinSample.Core.Entities
{
    [Table("Notes")]
    public class Note
    {
        [Key]
        public long Id { get; set; }

        public DateTime Created { get; set; }
        public string Text { get; set; }


        [ForeignKey(nameof(TaskId))]
        public TaskEntity Task { get; set; }
        public long TaskId { get; set; }
    }
}
