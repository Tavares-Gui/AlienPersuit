using System.Drawing;
using System.Windows.Forms;

public class Player
{
    public int PlayerLife { get; set; } = 0;
    public int PlayerDamage { get; set; } = 0;
    public int Seeds { get; set; } = 0;
    public int Tools { get; set; } = 0;
    public float Vx { get; set; }
    public float Vy { get; set; }
    public static float SizeX { get; set; } = 80;
    public static float SizeY { get; set; } = 95;
    public static Image Img { get; set; }
    public static int ImageIndex { get; set; }
    public static int SpeedIndex { get; set; }
    public static bool Movement { get; set; } = false;
    public static Image ImgPadrao { get; set; }

    public static Image[] playerAnim =
    {
        Bitmap.FromFile("./assets/player/1down.png"),
        Bitmap.FromFile("./assets/player/2down.png"),
        Bitmap.FromFile("./assets/player/3down.png"),
        Bitmap.FromFile("./assets/player/4up.png"),
        Bitmap.FromFile("./assets/player/5up.png"),
        Bitmap.FromFile("./assets/player/6up.png"),
        Bitmap.FromFile("./assets/player/7right.png"),
        Bitmap.FromFile("./assets/player/8right.png"),
        Bitmap.FromFile("./assets/player/9right.png"),
        Bitmap.FromFile("./assets/player/10left.png"),
        Bitmap.FromFile("./assets/player/11left.png"),
        Bitmap.FromFile("./assets/player/12left.png"),
    };

    public Player()
    {
        Img = playerAnim[0];
    }

    public void Draw(Graphics g, PictureBox pb)
    {

        g.DrawImage(Img, pb.Width / 2 - 75, pb.Height / 2 - 75, SizeX, SizeY);

        const int speed = 4;

        if (Movement == true)
        {
            if (SpeedIndex < speed)
            {
                Img = playerAnim[ImageIndex + 1];
                SpeedIndex++;
            }
            else
            {
                Img = playerAnim[ImageIndex + 2];
                SpeedIndex++;
                if (SpeedIndex >= 2 * speed)
                    SpeedIndex = 0;
            }
        }
    }

    // public void DrawStats(Graphics g, PictureBox pb)
    // {
    //     Color textColor = Color.White;
    //     SolidBrush textBrush = new(textColor);

    //     Font font = new("Arial", 12, FontStyle.Bold);

    //     g.DrawImage(Images.stats[0], pb.Width * 0.01f, pb.Height * 0.01f);
    //     g.DrawString(PlayerLife.ToString(), font, textBrush, new PointF(pb.Width * 0.06f, pb.Height * 0.05f));

    //     g.DrawImage(Images.stats[1], pb.Width * 0.01f, pb.Height * 0.08f);
    //     g.DrawString(Seeds.ToString(), font, textBrush, new PointF(pb.Width * 0.06f, pb.Height * 0.12f));
    // }

    public static void DrawRecord(Graphics g, PictureBox pb)
    {
        g.DrawString(
            $"Actual personal record: {Game.timesFinished - 1}.",
            SystemFonts.MenuFont,
            Brushes.White,
            new PointF(20, Game.Pb.Height - 100)
        );
    }
}