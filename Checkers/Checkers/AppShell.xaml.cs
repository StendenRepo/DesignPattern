using Checkers.Views;

namespace Checkers;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("home", typeof(HomePage));
        Routing.RegisterRoute("game", typeof(CheckersPage));
    }
}