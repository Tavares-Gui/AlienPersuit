using System;
using System.Drawing;
using System.Collections.Generic;

public class Chest
{
    public bool Open { get; set; } = false;
    public float X { get; set; }
    public float Y { get; set; }
    public float Size { get; set; } = 10;
    public List<Image> img { get; set; }

    public static Image[] chest =
    {
        Bitmap.FromFile("./assets/chests/flower1.png"),
        Bitmap.FromFile("./assets/chests/flower2.png")
    };

    public void Draw(bool open, string img1Path, string img2Path)
    {
        this.Open = open;
        this.img = new(){Bitmap.FromFile(img1Path), Bitmap.FromFile(img2Path)};
    }
}
