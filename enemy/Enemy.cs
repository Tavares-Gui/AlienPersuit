using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Enemy
{
    public int EnemyLife { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Size { get; set; } = 10;
    public float Speed { get; set; } = 5;

    public void Draw(Graphics g, PictureBox pb, Bitmap bmp)
    {
        g.DrawImage(Bitmap.FromFile("../assets/enemy/enemy1.png"), 5, 5, 5, 5);
    }
}
