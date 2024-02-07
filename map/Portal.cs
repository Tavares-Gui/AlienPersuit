using System;
using System.Drawing;
using System.Windows.Forms;

public class Portal
{
    public int X { get; set; }
    public int Y { get; set; }
    public static int SizeX { get; set; } = 140;
    public static int SizeY { get; set; } = 150;
    public static Image Img { get; set; } = Bitmap.FromFile("./assets/objects/portal.png");
    public static bool PortalCreated { get; set; } = false;

    public static void Draw(Graphics g, float x, float y)
    {
        Pen pen = new(Color.Red, 5f);

        g.DrawImage(Img, x - SizeX / 2, y - SizeY / 2, SizeX, SizeY);
        g.DrawRectangle(pen, x - SizeX / 2, y - SizeY / 2, SizeY, SizeY);
    }
}