using System.Drawing;

public class Menu
{
    public static Image Img { get; set; } = Bitmap.FromFile("./assets/menu/bg.png");

    public static void Draw(Graphics g)
    {
        g.DrawImage(Img, 0, 0, Game.Pb.Width, Game.Pb.Height);
    }
}