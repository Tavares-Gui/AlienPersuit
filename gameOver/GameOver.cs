using System.Drawing;

public class GameOver
{
    public static Image Img { get; set; } = Bitmap.FromFile("./assets/gameOver/goiabado.png");

    public static void Draw(Graphics g)
    {
        g.DrawImage(Img, 0, 0, Game.Pb.Width, Game.Pb.Height);
    }

    public static void DrawTimer(Graphics g)
    {
        int secs = Game.RemainingTime;

        g.DrawString($"Tempo restante: {secs / 1000}s", SystemFonts.MenuFont, Brushes.White, new PointF(20, Game.Pb.Height - 80));
    }
}