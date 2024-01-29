using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Enemy
{
    public int EnemyLife { get; set; } = 0;
    public int EnemyDamage { get; set; } = 0; 
    public float Speed { get; set; } = 0;
    public float X { get; set; }
    public float Y { get; set; }
    public float Size { get; set; } = 10;
    public List<Image> img { get; set; } = new List<Image>();

    public Image[] enemies =
    {
        Bitmap.FromFile("./assets/enemy/alisson.png"),
        Bitmap.FromFile("./assets/enemy/fer.png"),
        Bitmap.FromFile("./assets/enemy/moll.png"),
        Bitmap.FromFile("./assets/enemy/trevisan.png"),
        Bitmap.FromFile("./assets/enemy/dom.png"),
        Bitmap.FromFile("./assets/enemy/marcao.png"),
        Bitmap.FromFile("./assets/enemy/hamilton.png")
    };

    public void Draw(
        int enemyLife, 
        int speed,
        int enemyDamage, 
        string img1Path, 
        string img2Path,
        string img3Path,
        string img4Path,
        string img5Path,
        string img6Path,
        string img7Path
    )
    {
        this.EnemyLife = enemyLife;
        this.Speed = speed;
        this.EnemyDamage = enemyDamage;
        this.img = new(){
            Bitmap.FromFile(img1Path), Bitmap.FromFile(img2Path), Bitmap.FromFile(img3Path),
            Bitmap.FromFile(img4Path), Bitmap.FromFile(img5Path), Bitmap.FromFile(img6Path), 
            Bitmap.FromFile(img7Path)
        };
    }
}
