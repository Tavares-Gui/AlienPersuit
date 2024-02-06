using System.Drawing;
using System.Collections.Generic;

public class Menu
{
    public static Image Img { get; set; } = Bitmap.FromFile("./assets/menu/bg.png");

    public static void Draw(Graphics g)
    {
        Pen pen = new(Color.Red, 5f);

        g.DrawImage(Img, 0, 0, Game.Pb.Width, Game.Pb.Height);
    }
}