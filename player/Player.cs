using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Player
{
    public int PlayerLife { get; set; } = 0;
    public int PlayerDamage { get; set; } = 0;
    public int Seeds { get; set; } = 0;
    public int Tools { get; set; } = 0;
    public float Vx { get; set; }
    public float Vy { get; set; }
    public float Size { get; set; } = 150;
    public List<Image> img { get; set; } = new List<Image>();

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

    public void Draw(Graphics g, PictureBox pb)
    {
        g.DrawImage(playerAnim[0], 
            pb.Width / 2 - 75, 
            pb.Height / 2 -75 ,
            Size, Size
        );
    }

    public void DrawStats(Graphics g, PictureBox pb)
    {
        Color textColor = Color.White;
        SolidBrush textBrush = new(textColor);

        Font font = new("Arial", 12, FontStyle.Bold);

        g.DrawImage(Images.stats[0], pb.Width * 0.01f, pb.Height * 0.01f);
        g.DrawImage(Images.stats[1], pb.Width * 0.06f, pb.Height * 0.01f);
        g.DrawString(PlayerLife.ToString(), font, textBrush, new PointF(pb.Width * 0.05f, pb.Height * 0.05f));
        g.DrawString(Seeds.ToString(), font, textBrush, new PointF(pb.Width * 0.10f, pb.Height * 0.05f));
    }
}