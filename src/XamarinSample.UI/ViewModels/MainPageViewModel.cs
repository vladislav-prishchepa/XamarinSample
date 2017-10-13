// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITCC.Logging.Core;
using ITCC.UI.Commands;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using XamarinSample.Core.Data;
using XamarinSample.Core.Entities;
using XamarinSample.Core.Utils;

namespace XamarinSample.UI.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private TaskListItemViewModel _selectedTask;
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (_isLoading == value)
                    return;

                _isLoading = value;
                OnPropertyChanged();

                ResheshCommand?.RaiseCanExecuteChanged();
                ShowInfoCommand?.RaiseCanExecuteChanged();
            }
        }
        public TaskListItemViewModel SelectedTask
        {
            get => _selectedTask;
            set
            {
                if (_selectedTask == value)
                    return;

                _selectedTask = value;
                OnPropertyChanged();

                ShowInfoCommand?.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<TaskListItemViewModel> Tasks { get; } = new ObservableCollection<TaskListItemViewModel>();

        public AsyncCommand ResheshCommand { get; }
        public DelegateCommand ShowInfoCommand { get; }

        public MainPageViewModel()
        {
            ResheshCommand = AsyncCommandFactory.Create(LoadDataAsync, () => !IsLoading);
            ShowInfoCommand = new DelegateCommand(
                () => MessagingCenter.Send(this, "ShowTaskInfoMessage", GetTaskInfo(SelectedTask)),
                () => !IsLoading && SelectedTask != null);
        }

        public async Task LoadDataAsync()
        {
            Cancel();
            try
            {
                IsLoading = true;

                using (var dbContext = new MobileDbContext("Database.db"))
                {
                    var dbInitSucceeded = await DataGenerator.EnsureDataCreatedAsync(dbContext, _cancellationTokenSource.Token).ConfigureAwait(false);
                    if (!dbInitSucceeded)
                    {
                        Logger.LogEntry("MAINPAGE", LogLevel.Error, "Database initialization failed.");
                        return;
                    }

                    var loadDataResult = await DbOperationsWrappers.PerformSafelyAsync(dbContext, async db =>
                    {
                        var data = await db.Tasks.Include(task => task.Notes).AsNoTracking()
                            .ToListAsync(_cancellationTokenSource.Token);

                        return new ErrorOr<List<TaskEntity>>(data);
                    }).ConfigureAwait(false);

                    if (loadDataResult.IsError)
                    {
                        Logger.LogEntry("MAINPAGE", LogLevel.Error, "Data loading failed.");
                        Logger.LogException("MAINPAGE", LogLevel.Error, loadDataResult.Error.Exception);
                        return;
                    }

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var selectedTaskId = SelectedTask?.Model.Id;

                        SelectedTask = null;
                        Tasks.Clear();

                        foreach (var taskEntity in loadDataResult.Success.OrderBy(task => task.Id))
                        {
                            Tasks.Add(new TaskListItemViewModel(taskEntity));
                        }

                        if (selectedTaskId.HasValue)
                        {
                            SelectedTask = Tasks.FirstOrDefault(task => task.Model.Id == selectedTaskId.Value);
                        }
                    });
                }
            }
            catch (OperationCanceledException)
            {

            }
            finally
            {
                Device.BeginInvokeOnMainThread(() => IsLoading = false);
            }
        }
        public void Cancel()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private static string GetTaskInfo([NotNull] TaskListItemViewModel taskListItem)
        {
            var infoBuilder = new StringBuilder(taskListItem.Model.Title);
            if (!string.IsNullOrWhiteSpace(taskListItem.Model.Description))
            {
                infoBuilder.AppendLine($"Description: {taskListItem.Model.Description}");
            }

            if (taskListItem.Model.Notes.Any())
            {
                infoBuilder.AppendLine("Notes:");
                foreach (var note in taskListItem.Model.Notes)
                {
                    infoBuilder.AppendLine(note.Text);
                }
            }

            return infoBuilder.ToString();
        }
    }
}
