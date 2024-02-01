using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class Lantern
{
    public void Draw(Graphics g, PictureBox pb)
    {
        const float min = .5f;
        const float max = .9f;

        const int erro = 0; // ????
        GraphicsPath path = new GraphicsPath();

        float radius = MathF.Sqrt(pb.Width * pb.Width + pb.Height * pb.Height) / 2;

        path.AddEllipse(
            pb.Width / 2 - radius - erro,
            pb.Height / 2 - radius,
            2 * radius + 2 * erro, 2 * radius + 2 * erro
        );

        ColorBlend blend = new ColorBlend();
        blend.Colors = new Color[] {
            Color.FromArgb(255, 0, 0, 0),
            Color.FromArgb(255, 0, 0, 0),
            Color.FromArgb(0, 0, 0, 0),
            Color.FromArgb(0, 0, 0, 0),
        };
        blend.Positions = new float[] {
            0f,
            min,
            max,
            1f
        };


        var brush = new PathGradientBrush(path)
        {
            InterpolationColors = blend
        };

        g.FillRectangle(brush, new Rectangle(0, 0, pb.Width, pb.Height));
    }
}