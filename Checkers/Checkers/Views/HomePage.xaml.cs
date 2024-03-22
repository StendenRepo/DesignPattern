using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.CheckersLogic;

namespace Checkers.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void Button_OnClicked1(object sender, EventArgs e)
    {
        var settings = new GameSettings(CheckersLogic.GameMode.Single, Difficulty.None);
        await Navigation.PushAsync(new CheckersPage(settings));
    }
    
    private void Button_OnClicked2(object sender, EventArgs e)  
    {
        this.Single.IsVisible = false;
        this.Double.IsVisible = false;
        this.GameMode.IsVisible = false;
        this.SelectDifficulty.IsVisible = true;
        this.BasicDifficulty.IsVisible = true;
        this.AdvancedDifficulty.IsVisible = true;
    }
    
    private async void Button_OnClicked3(object sender, EventArgs e)
    {
        var settings = new GameSettings(CheckersLogic.GameMode.Double, Difficulty.Basic);
        await Navigation.PushAsync(new CheckersPage(settings));
        this.ResetVisibility();
    }
    
    private async void Button_OnClicked4(object sender, EventArgs e)
    {
        var settings = new GameSettings(CheckersLogic.GameMode.Double, Difficulty.Advanced);
        await Navigation.PushAsync(new CheckersPage(settings));
        this.ResetVisibility();
    }

    private void ResetVisibility()
    {
        this.Single.IsVisible = true;
        this.Double.IsVisible = true;
        this.GameMode.IsVisible = true;
        this.SelectDifficulty.IsVisible = false;
        this.BasicDifficulty.IsVisible = false;
        this.AdvancedDifficulty.IsVisible = false;
    }
}