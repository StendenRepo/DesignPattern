using Checkers.Views;

namespace Checkers;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new HomePage());
    }
}