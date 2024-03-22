using Checkers.CheckersLogic;
using Checkers.ViewModels;

namespace Checkers.Views;

public partial class CheckersPage : ContentPage
{
	public CheckersPage(GameSettings settings)
	{
		InitializeComponent();
		BindingContext = new CheckersViewModel(settings);
	}
}