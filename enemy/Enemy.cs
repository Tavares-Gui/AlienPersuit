using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Enemy
{
    public int EnemyLife { get; set; } = 0;
    public int EnemyDamage { get; set; } = 0; 
    public float EnemySpeed { get; set; } = 0;
    public static Random random { get; set; } = new Random();
    public float X { get; set; }
    public float Y { get; set; }
    public float Size { get; set; } = 200;
    public List<Image> img { get; set; } = new List<Image>();
    public Space space { get; set; }

    public Enemy(Image img ,int enemyLife, int enemySpeed, int enemyDamage)
    {
        this.img.Add(img);
        EnemyLife = enemyLife;
        EnemySpeed = enemySpeed;
        EnemyDamage = enemyDamage;

        this.X = random.Next(0, Game.Pb.Width - (int)this.Size);
        this.Y = random.Next(0, Game.Pb.Height - (int)this.Size);
    }

    public static List<Enemy> Enemies { get; set; } = new List<Enemy>
    {
        new Enemy(Bitmap.FromFile("./assets/enemy/weak/alisson.png"), 3, 4, 1),
        new Enemy(Bitmap.FromFile("./assets/enemy/weak/fer.png"), 4, 6, 1),
        new Enemy(Bitmap.FromFile("./assets/enemy/weak/moll.png"), 5, 4, 1),
        new Enemy(Bitmap.FromFile("./assets/enemy/weak/dom.png"), 4, 4, 2),
        new Enemy(Bitmap.FromFile("./assets/enemy/weak/hamilton.png"), 1, 1, 1),
        new Enemy(Bitmap.FromFile("./assets/enemy/strong/trevisan.png"), 7, 4, 7),
        new Enemy(Bitmap.FromFile("./assets/enemy/strong/domDOr.png"), 7, 6, 8),
        new Enemy(Bitmap.FromFile("./assets/enemy/strong/queila.png"), 8, 7, 9),
        new Enemy(Bitmap.FromFile("./assets/enemy/strong/marcao.png"), 7, 6, 10),
        new Enemy(Bitmap.FromFile("./assets/enemy/strong/hamiltonDOr.png"), 10, 10, 10)
    };
}