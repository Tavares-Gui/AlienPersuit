using System.Drawing;
using System.Windows.Forms;

public class Portal
{
    public int X { get; set; }
    public int Y { get; set; }
    public static int SizeX { get; set; } = 110;
    public static int SizeY { get; set; } = 150;
    public static Image Img { get; set; } = Bitmap.FromFile("./assets/objects/portal.png");
    public static bool PortalCreated { get; set; } = false;
}