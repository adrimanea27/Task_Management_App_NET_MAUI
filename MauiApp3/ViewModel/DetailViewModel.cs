//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using MauiApp2.Model;
//using System.Collections.ObjectModel;
//using System.IO;
//using System.Threading.Tasks;

//namespace MauiApp2.ViewModel
//{
//    [QueryProperty("TaskId", "TaskId")]


//    public partial class DetailViewModel : ObservableObject
//    {




//        [ObservableProperty]
//        public ObservableCollection<DetailItem> details = new ObservableCollection<DetailItem>();

//        [ObservableProperty]
//        public string text;

//        [ObservableProperty]
//        public string detail;



//        [RelayCommand]
//        public Task AddDetail()
//        {
//            if (string.IsNullOrWhiteSpace(Detail))
//                return Task.CompletedTask;
//            var newDetail = new DetailItem { DetailText = Detail };
//            //ait _database.SaveDetailAsync(newDetail);
//            Details.Add(newDetail);

//            Detail = string.Empty;
//            return Task.CompletedTask;
//        }
//    }
//}


using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiApp3.ViewModel
{
    [QueryProperty("TaskId", "taskId")]
    public partial class DetailViewModel : ObservableObject
    {
        private readonly DetailDatabase _detailDatabase;
        private string _detailText;

        public DetailViewModel()
        {
            _detailDatabase = new DetailDatabase();
            Details = new ObservableCollection<DetailItem>();

            
            AddDetailCommand = new AsyncRelayCommand(AddDetailAsync);
        }

        [ObservableProperty]
        private int taskId; 

        public string DetailText
        {
            get => _detailText;
            set => SetProperty(ref _detailText, value);
        }

        public ObservableCollection<DetailItem> Details { get; }

        public ICommand AddDetailCommand { get; }

        // Method for adding a detail in database
        private async Task AddDetailAsync()
        {
            if (!string.IsNullOrWhiteSpace(DetailText))
            {
                var detail = new DetailItem { DetailText = DetailText, TaskItemId = TaskId }; 
                await _detailDatabase.SaveDetailAsync(detail);

                Details.Add(detail); 
               // DetailText = string.Empty; 
            }
        }

        
        partial void OnTaskIdChanged(int value)
        {
            LoadDetailsByIdAsync(value);
        }

        private async Task LoadDetailsByIdAsync(int taskId)
        {
            
            var details = await _detailDatabase.GetDetailsByTaskIdAsync(taskId);
           // Details.Clear(); //Delete old detaild
            foreach (var detail in details)
            {
                Details.Add(detail);
            }
        }
    }
}


