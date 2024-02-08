using System;
using System.Drawing;
using System.Windows.Forms;

public class Portal
{
    public static float X { get; set; }
    public static float Y { get; set; }
    public static int SizeX { get; set; } = 140;
    public static int SizeY { get; set; } = 150;
    public static Image Img { get; set; } = Bitmap.FromFile("./assets/objects/portal.png");
    public static bool PortalCreated { get; set; } = false;
    private static RectangleF PortalHitbox = new RectangleF();

    public static void Draw(Graphics g, float x, float y)
    {
        PortalHitbox = new RectangleF(x - SizeX / 2, y - SizeY / 2, SizeX, SizeY);

        X = x;
        Y = y;

        g.DrawImage(Img, PortalHitbox);
    }

    public bool HasPlayer(RectangleF player, Space space)
    {
        RectangleF playerHitbox = new RectangleF(player.X, player.Y, Player.SizeX, Player.SizeY);
        PortalHitbox = new RectangleF(X - SizeX / 2, Y - SizeY / 2, SizeX, SizeY);

        if (PortalHitbox.Contains(playerHitbox))
            return true;

        return false;
    }
}