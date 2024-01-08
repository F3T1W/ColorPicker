namespace ColorPicker;

using System.Windows.Forms;
public partial class MainForm : Form
{
    private Label? redLabel;
    private Label? greenLabel;
    private Label? blueLabel;
    private PictureBox? colorPreview;

    public MainForm()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        redLabel = new Label
        {
            Text = "R: 0",
            Location = new Point(10, 10),
            ForeColor = Color.Red
        };

        greenLabel = new Label
        {
            Text = "G: 0",
            Location = new Point(10, 30),
            ForeColor = Color.Green
        };

        blueLabel = new Label
        {
            Text = "B: 0",
            Location = new Point(10, 50),
            ForeColor = Color.Blue
        };

        colorPreview = new PictureBox
        {
            Location = new Point(10, 70),
            Size = new Size(50, 50),
            BackColor = Color.White
        };

        Controls.Add(redLabel);
        Controls.Add(greenLabel);
        Controls.Add(blueLabel);
        Controls.Add(colorPreview);

        Timer timer = new()
        {
            Interval = 10
        };
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        Point cursor = PointToClient(Cursor.Position);
        Color pixelColor = GetPixelColor(cursor);
        UpdateLabels(pixelColor);
        UpdateColorPreview(pixelColor);
    }

    private static Color GetPixelColor(Point cursor)
    {
        try
        {
            Bitmap bmp = new Bitmap(1, 1);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(cursor, Point.Empty, new Size(1, 1));
            }

            return bmp.GetPixel(0, 0);
        }
        catch (Exception)
        {
            return Color.Black; // Default color when an exception occurs
        }
    }

    private void UpdateLabels(Color color)
    {
        redLabel!.Text = $"R: {color.R}";
        greenLabel!.Text = $"G: {color.G}";
        blueLabel!.Text = $"B: {color.B}";
    }

    private void UpdateColorPreview(Color color)
    {
        // Create a new RGB color using values from the labels
        int red = int.Parse(redLabel!.Text.Split(' ')[1]);
        int green = int.Parse(greenLabel!.Text.Split(' ')[1]);
        int blue = int.Parse(blueLabel!.Text.Split(' ')[1]);

        Color newColor = Color.FromArgb(red, green, blue);

        colorPreview!.BackColor = newColor;
    }
}