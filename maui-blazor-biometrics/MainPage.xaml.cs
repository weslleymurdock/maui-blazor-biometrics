using maui_blazor_biometrics.ViewModels; 
namespace maui_blazor_biometrics;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        RootBlazorWebView.Parameters = new Dictionary<string, object>
        {
            { "ViewModel", viewModel }
        }!;
    }
 
}
