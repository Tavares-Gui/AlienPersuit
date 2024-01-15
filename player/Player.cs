using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Player
{
    public int Lifes { get; set; }
    public int Seeds { get; set; }
    public int Tools { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Size { get; set; } = 10;
    public float Speed { get; set; } = 5;
    public float RealMove { get; set; } = 0f;

    public void PlayerAndInfo(Graphics g, Player player, PictureBox pb)
    {
        Label seeds = new()
        {
            Text = $"{player.Seeds}",
            Location = new Point(10, 50),
            Width = 300,
            Height = 50,
        };

        Label lifes = new()
        {
            Text = $"{player.Lifes}",
            Location = new Point(10, 0),
            Width = 300,
            Height = 50,
        };
        pb.Controls.Add(seeds);
        pb.Controls.Add(lifes);
    }
    
    public void StartLeft()
        => RealMove = -Speed;

    public void StartRight()
        => RealMove = Speed;

    public void Move(int xmin, int xmax)
    {
        X += RealMove;

        if (X < xmin)
            X = xmin;
        if (X > xmax)
            X = xmax;

        RealMove *= 0.9f;
    }
}
