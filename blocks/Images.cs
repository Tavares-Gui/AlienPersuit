using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Images
{
    public List<Image> img { get; set; } = new List<Image>();

    public static Image[] floors =
    {
        Bitmap.FromFile("./assets/blocks/floor1.png"),
        Bitmap.FromFile("./assets/blocks/floor2.png"),
        Bitmap.FromFile("./assets/blocks/floor3.png"),
        Bitmap.FromFile("./assets/blocks/floor4.png"),
        Bitmap.FromFile("./assets/blocks/floor5.png"),
        Bitmap.FromFile("./assets/blocks/floor6.png"),
        Bitmap.FromFile("./assets/blocks/floor7.png"),
        Bitmap.FromFile("./assets/blocks/floor8.png"),
        Bitmap.FromFile("./assets/blocks/floor9.png"),
        Bitmap.FromFile("./assets/blocks/floor10.png"),
        Bitmap.FromFile("./assets/blocks/floor11.png"),
        Bitmap.FromFile("./assets/blocks/floor12.png"),
        Bitmap.FromFile("./assets/blocks/floor13.png"),
        Bitmap.FromFile("./assets/blocks/floor14.png"),
        Bitmap.FromFile("./assets/blocks/floor15.png")
    };

    public static Image[] wall = 
    {
        Bitmap.FromFile("./assets/blocks/wall.png")
    };

    public static Image[] stats = 
    {
        Bitmap.FromFile("./assets/objects/heart.png"),
        Bitmap.FromFile("./assets/objects/seed.png")
    };

    public void Draw
    (
        string img1Path,  string img2Path, string img3Path, string img4Path, string img5Path,
        string img6Path, string img7Path, string img8Path, string img9Path, string img10Path,
        string img11Path,  string img12Path, string img13Path, string img14Path, string img15Path,
        string img16Path, string img17Path, string img18Path
    )
    {
        this.img = new(){
            Bitmap.FromFile(img1Path), Bitmap.FromFile(img2Path), Bitmap.FromFile(img3Path), Bitmap.FromFile(img4Path), Bitmap.FromFile(img5Path), 
            Bitmap.FromFile(img6Path), Bitmap.FromFile(img7Path), Bitmap.FromFile(img8Path), Bitmap.FromFile(img9Path), Bitmap.FromFile(img10Path),
            Bitmap.FromFile(img11Path), Bitmap.FromFile(img12Path), Bitmap.FromFile(img13Path), Bitmap.FromFile(img14Path), Bitmap.FromFile(img15Path),
            Bitmap.FromFile(img16Path), Bitmap.FromFile(img17Path), Bitmap.FromFile(img18Path) 
        };
    }
}