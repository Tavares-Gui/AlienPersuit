using System;
using System.Drawing;
using System.Windows.Forms;

public class Player
{
    public int PlayerLife { get; set; }
    public int Seeds { get; set; }
    public int Tools { get; set; }
    public float Vx { get; set; }
    public float Vy { get; set; }
    public float Speed { get; set; } = 5;

    public void Draw(Graphics g, PictureBox pb, Bitmap bmp)
    {
        g.DrawImage(Bitmap.FromFile("../assets/player/downDefault.png"), 5, 5, 5, 5);
    }
}
