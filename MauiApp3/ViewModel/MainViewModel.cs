using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
//using MauiApp3.ViewModel;
//using SQLite;
//using System.IO;
//using System.Threading.Tasks;
//using System;
//using static System.Net.Mime.MediaTypeNames;

namespace MauiApp3.ViewModel
{
  
    public partial class MainViewModel : ObservableObject
    {
        private readonly IConnectivity connectivity;
        private readonly TaskDatabase _database;

        public MainViewModel(IConnectivity connectivity)
        {
            this.connectivity = connectivity;
            Items = new ObservableCollection<TaskItem>();

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tasks.db");
            _database = new TaskDatabase(dbPath);

            LoadTasks();
        }

        [ObservableProperty]
        private ObservableCollection<TaskItem> items;

        [ObservableProperty]
        private string text;

        private async void LoadTasks()
        {
            var tasks = await _database.GetTasksAsync();
            Items.Clear();
            foreach (var task in tasks)
            {
                Items.Add(task);
            }
        }

        [RelayCommand]
        private async Task Add()
        {
            if (string.IsNullOrWhiteSpace(Text))
                return;

            // Check for internet connection
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Oh, no!",
                    "It looks like you have no internet connection.\n" +
                    "Please check:\n" +
                    "- Your Wi-Fi router\n" +
                    "- Mobile data\n" +
                    "- Restart the app if necessary.",
                    "Got it!");
                return;
            }

            var newTask = new TaskItem { Title = Text };
            await _database.SaveTaskAsync(newTask);

            Items.Add(newTask);
            Text = string.Empty; // Clear the input after adding
        }

        [RelayCommand]
        private async Task Delete(TaskItem task)
        {
            if (task == null) return;

            await _database.DeleteTaskAsync(task);
            Items.Remove(task); // Remove task from the list
        }

        [RelayCommand]
        private async Task Tap(TaskItem task)
        {
            if (task == null) return;

            // Navigate to DetailPage, passing TaskId
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?taskId={task.Id}");
        }


    }
}
