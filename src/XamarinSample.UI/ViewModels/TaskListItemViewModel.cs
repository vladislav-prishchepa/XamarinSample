// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using JetBrains.Annotations;
using XamarinSample.Core.Entities;

namespace XamarinSample.UI.ViewModels
{
    public class TaskListItemViewModel
    {
        public TaskEntity Model { get; }

        public string Title { get; }
        public DateTime Start { get; }
        public DateTime End { get; }

        public TaskListItemViewModel([NotNull] TaskEntity task)
        {
            Model = task;

            Title = task.Title;
            Start = task.Start;
            End = task.End;
        }
    }
}
