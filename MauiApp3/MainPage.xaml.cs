using MauiApp3.ViewModel;

namespace MauiApp3
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = (vm);

        }


    }

}
