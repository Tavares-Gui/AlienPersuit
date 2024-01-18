using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Enemy
{
    public int EnemyLife { get; set; } = 3;
    public float X { get; set; }
    public float Y { get; set; }
    public float Size { get; set; } = 10;
    public float Speed { get; set; } = 4;
    // public List<Image> img { get; set; }

    public void Draw(int enemyLife, int speed, string img1Path, string img2Path)
    {
        this.EnemyLife = enemyLife;
        this.Speed = speed;
        // this.img = new(){Bitmap.FromFile(img1Path), Bitmap.FromFile(img2Path)};
    }
}
