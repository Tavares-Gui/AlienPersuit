using System.Drawing;
using System.Collections.Generic;

public class Menu
{
    public static Image Img { get; set; } = Bitmap.FromFile("./assets/menu/bg.png");

    public static List<PointF[]> Buttons { get; set; } = new(){
        new PointF[]{
            new(Game.Pb.Width * 0.285f,  Game.Pb.Height * 0.51f),
            new(Game.Pb.Width * 0.285f,  Game.Pb.Height * 0.51f + Game.Pb.Height * 0.035f),
            new(Game.Pb.Width * 0.285f + Game.Pb.Width * 0.13f,  Game.Pb.Height * 0.50f + Game.Pb.Height * 0.035f),
            new(Game.Pb.Width * 0.285f + Game.Pb.Width * 0.13f,  Game.Pb.Height * 0.50f),
        },
    };
   

    public static bool Clicks(PointF point, PointF[] button)
    {
        int num_vertices = button.Length;
        double x = point.X, y = point.Y;
        bool inside = false;

        PointF p1 = button[0], p2;

        for (int i = 1; i <= num_vertices; i++)
        {
            p2 = button[i % num_vertices];

            float miny = p1.Y;
            if (p2.Y < p1.Y) miny = p2.Y;

            float maxy = p1.Y;
            if (p2.Y > p1.Y) maxy = p2.Y;

            float maxx = p1.X;
            if (p2.X > p1.X) maxx = p2.X;

            if (y > miny && y <= maxy && x <= maxx)
            {
                double x_intersection =
                (y - p1.Y) * (p2.X - p1.X) /
                (p2.Y - p1.Y) + p1.X;

                if (p1.X == p2.X || x <= x_intersection)
                    inside = !inside;
            }

            p1 = p2;
        }

        if (inside)
        {
            if (point.Y < Game.Pb.Height * 0.545)
            {
                Game.GameStart = true;
            }
        }
        return inside;
    }

    public static void ClickCheckAll(PointF point)
    {
        Clicks(point, Buttons[0]);
    }

    public static void Draw(Graphics g)
    {
        Pen pen = new(Color.Red, 5f);

        g.DrawImage(Menu.Img, 0, 0, Game.Pb.Width, Game.Pb.Height);
        g.DrawRectangle(pen, Game.Pb.Width * 0.265f, Game.Pb.Height * 0.43f, Game.Pb.Width * 0.16f, Game.Pb.Height * 0.2f);
    }
}