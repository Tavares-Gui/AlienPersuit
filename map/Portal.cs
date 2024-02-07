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
    private static RectangleF PortalHitbox = new RectangleF();

    public static void Draw(Graphics g, float x, float y)
    {
        Pen pen = new(Color.Red, 5f);
        PortalHitbox = new RectangleF(x - SizeX / 2, y - SizeY / 2, SizeX, SizeY);

        g.DrawImage(Img, PortalHitbox);
        g.DrawRectangle(pen, PortalHitbox);
    }

    // public bool HasPlayer(RectangleF player, float x, float y)
    //     => hasPlayer(player, x, y);

    // private bool hasPlayer(RectangleF player, float x, float y)
    // {
    //     RectangleF playerHitbox = new RectangleF(player.X, player.Y, Player.SizeX, Player.SizeY);
    //     RectangleF portalHitbox = new RectangleF(x - SizeX / 2, y - SizeY / 2, SizeY, SizeY);

    //     if (portalHitbox.Contains(playerHitbox))
    //     {
    //         return true;
    //     }

    //     return false;
    // }

    public bool HasPlayer(RectangleF player, float x, float y, Space crrSpace)
        => hasPlayer(player, x, y, crrSpace);

    private bool hasPlayer(RectangleF player, float x, float y, Space space)
    {
        RectangleF playerHitbox = new RectangleF(player.X, player.Y, Player.SizeX, Player.SizeY);
        PortalHitbox = new RectangleF(x - SizeX / 2, y - SizeY / 2, SizeX, SizeY);

        if (PortalHitbox.Contains(playerHitbox))
            return true;

        return false;
    }
}