using ABI.Microsoft.UI.Xaml.Shapes;

namespace Checkers.Views;

public partial class TileView : ContentView
{
    public static readonly BindableProperty TileColorProperty =
        BindableProperty.Create(nameof(TileColor), typeof(Color), typeof(TileView), Color.Parse("Red"));

    public Color TileColor
    {
        get => (Color)GetValue(TileColorProperty);

        set => SetValue(TileColorProperty, value);
    }

    public TileView()
	{
        InitializeComponent();
    }

    public void OnTapGestureRecognizerTapped(object sender, EventArgs e)
    {
        this.TileColor = Color.Parse("Yellow");
    }
    //TODO: Zorg dat de TapGesture command in de ViewModel komt en dat het hele board wordt aangemaakt. Kan in principe 64 TileViews met elk een eigen Row en Column property?
    //TODO: Zorg dat de Tile model gelinkt wordt aan de TileView view. 
}