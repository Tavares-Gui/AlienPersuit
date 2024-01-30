using System;
using System.Collections.Generic;
using System.Drawing;

public class Player
{
    public int PlayerLife { get; set; } = 0;
    public int PlayerDamage { get; set; } = 0;
    public int Seeds { get; set; } = 0;
    public int Tools { get; set; } = 0;
    public float Vx { get; set; }
    public float Vy { get; set; }
    public List<Image> img { get; set; } = new List<Image>();

    public static Image[] playerAnim =
    {
        Bitmap.FromFile("./assets/player/1down.png"),
        Bitmap.FromFile("./assets/player/2down.png"),
        Bitmap.FromFile("./assets/player/3down.png"),
        Bitmap.FromFile("./assets/player/4up.png"),
        Bitmap.FromFile("./assets/player/5up.png"),
        Bitmap.FromFile("./assets/player/6up.png"),
        Bitmap.FromFile("./assets/player/7right.png"),
        Bitmap.FromFile("./assets/player/8right.png"),
        Bitmap.FromFile("./assets/player/9right.png"),
        Bitmap.FromFile("./assets/player/10left.png"),
        Bitmap.FromFile("./assets/player/11left.png"),
        Bitmap.FromFile("./assets/player/12left.png"),
    };

    public void Draw(
        int playerLife, 
        int seeds, 
        string img1Path, string img2Path, string img3Path, 
        string img4Path, string img5Path, string img6Path, 
        string img7Path, string img8Path, string img9Path, 
        string img10Path, string img11Path, string img12Path
    )
    {
        this.PlayerLife = playerLife;
        this.Seeds = seeds;
        this.img = new(){
            Bitmap.FromFile(img1Path), Bitmap.FromFile(img2Path), Bitmap.FromFile(img3Path),
            Bitmap.FromFile(img4Path), Bitmap.FromFile(img5Path), Bitmap.FromFile(img6Path),
            Bitmap.FromFile(img7Path), Bitmap.FromFile(img8Path), Bitmap.FromFile(img9Path),
            Bitmap.FromFile(img10Path), Bitmap.FromFile(img11Path), Bitmap.FromFile(img12Path)
        };
    }
}