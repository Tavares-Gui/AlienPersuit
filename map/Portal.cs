using System.Drawing;
using System.Windows.Forms;

public class Portal
{
    public int X { get; set; }
    public int Y { get; set; }
    public int SizeX { get; set; }
    public int SizeY { get; set; }
    public static Image Img { get; set; }

    public Portal()
    {
        Img = Bitmap.FromFile("./assets/objects/portal.png");
    }

    public void Draw(Graphics g, PictureBox pb, float x, float y)
    {
        g.DrawImage(Img, x, y, SizeX, SizeY); 
    }
}