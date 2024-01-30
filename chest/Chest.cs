using System;
using System.Drawing;
using System.Collections.Generic;

public class Chest
{
    public bool Open { get; set; } = false;
    public float X { get; set; }
    public float Y { get; set; }
    public float Size { get; set; } = 200;
    public int ChestDamage { get; set; } = 0;
    public int DecreaseSeedSpeed { get; set; } = 0;
    public int SubSeeds { get; set; } = 0;
    public int DeacreaseSpeed { get; set; } = 0;
    public int IncreaseSpeed { get; set; } = 0;
    public int AddLife { get; set; } = 0;
    public int AddSeeds { get; set; } = 0;
    public int IncreaseSeedSpeed { get; set; } = 0;
    public List<Image> img { get; set; } = new List<Image>();

    public Chest
    (
        Image img,
        int chestDamage, int decreaseSpeed, int deacreaseSeedSpeed, int subSeeds,
        int addLife, int increaseSpeed, int increaseSeedSpeed, int addSeeds
    )
    {
        this.img.Add(img);
        ChestDamage = chestDamage;
        DeacreaseSpeed = decreaseSpeed;
        DecreaseSeedSpeed = deacreaseSeedSpeed;
        SubSeeds = subSeeds;
        AddLife = addLife;
        IncreaseSpeed = increaseSpeed;
        IncreaseSeedSpeed = increaseSeedSpeed;
        AddSeeds = addSeeds;
    }

    public static List<Chest> Chests { get; set; } = new List<Chest>
    {
        //Bad rewards
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 4, 30, 15, 3, 0, 0, 0, 0),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 3, 20, 10, 2, 0, 0, 0, 0),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 2, 10, 5, 1, 0, 0, 0, 0),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 5, 35, 18, 4, 0, 0, 0, 0),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 4, 25, 12, 3, 0, 0, 0, 0),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 3, 15, 7, 2, 0, 0, 0, 0),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 2, 10, 5, 1, 0, 0, 0, 0),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 1, 5, 3, 1, 0, 0, 0, 0),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 2, 10, 5, 1, 0, 0, 0, 0),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 3, 15, 7, 2, 0, 0, 0, 0),

        //Good rewards
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 0, 0, 0, 0, 6, 30, 15, 10),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 0, 0, 0, 0, 4, 20, 10, 8),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 0, 0, 0, 0, 2, 10, 5, 6),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 0, 0, 0, 0, 8, 35, 18, 12),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 0, 0, 0, 0, 5, 25, 12, 9),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 0, 0, 0, 0, 3, 15, 7, 5),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 0, 0, 0, 0, 6, 30, 15, 10),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 0, 0, 0, 0, 4, 20, 10, 8),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 0, 0, 0, 0, 2, 10, 5, 6),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 0, 0, 0, 0, 8, 35, 18, 12),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 0, 0, 0, 0, 5, 25, 12, 9),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 0, 0, 0, 0, 3, 15, 7, 5),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 0, 0, 0, 0, 6, 30, 15, 10),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 0, 0, 0, 0, 4, 20, 10, 8),
        new Chest(Bitmap.FromFile("./assets/chests/flower1.png"), 0, 0, 0, 0, 2, 10, 5, 6),
    };
}
