namespace maui_blazor_biometrics;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

		MainPage = serviceProvider.GetRequiredService<MainPage>();
    }
}
