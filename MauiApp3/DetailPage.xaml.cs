//using AndroidX.Lifecycle;
using MauiApp3.ViewModel;

namespace MauiApp3;

public partial class DetailPage : ContentPage
{
    public DetailPage(DetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

    }
}