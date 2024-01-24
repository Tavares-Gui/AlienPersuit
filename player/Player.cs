using System;
using System.Collections.Generic;
using System.Drawing;

public class Player
{
    public int PlayerLife { get; set; } = 3;
    public int Seeds { get; set; } = 4;
    public int Tools { get; set; }
    public float Vx { get; set; }
    public float Vy { get; set; }
    public float Speed { get; set; } = 5;
    public List<Image> img { get; set; } = new List<Image>();

    public void Draw(int playerLife, int seeds, int speed, string img1Path)
    {
        this.PlayerLife = playerLife;
        this.Seeds = seeds;
        this.Speed = speed;
        this.img = new(){Bitmap.FromFile(img1Path)};
    }
}
