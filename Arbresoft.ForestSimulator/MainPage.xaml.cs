using Arbresoft.ForestSimulator.Models;
using Point = Microsoft.Maui.Graphics.Point;

namespace Arbresoft.ForestSimulator;

public partial class MainPage : ContentPage
{
    ForestDrawing drawing;

    public MainPage()
	{
		InitializeComponent();
        this.drawing = (ForestDrawing)MyCanvas.Drawable;
    }

	public void OnCounterClicked(object sender, EventArgs e)
    {
        CounterBtn.IsEnabled = false;
        CounterBtn.Text = "Pending simulation";
        SemanticScreenReader.Announce(CounterBtn.Text);

        drawing.AddTree(new Pinus(GetRandomPoint()));
        drawing.AddTree(new Quercus(GetRandomPoint()));
        drawing.AddTree(new Abies(GetRandomPoint()));

        Task.Run(() =>
        {
            while(true)
            {
                drawing.AddOneYear();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    MyCanvas.Invalidate();
                });
            }
        });
    }

    private Point GetRandomPoint(int maxX = 500, int maxY = 500)
    {
        Random random = new Random();

        // Define the range for X and Y coordinates
        int minX = 0; // Minimum X coordinate
        int minY = 0; // Minimum Y coordinate

        // Generate random X and Y coordinates within the defined range
        int randomX = random.Next(minX, maxX);
        int randomY = random.Next(minY, maxY);

        return new Point(randomX, randomY);
    }
}
