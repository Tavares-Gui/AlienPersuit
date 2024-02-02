using System.IO;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

public static class Resources
{
    public static void New()
    { 
        PlayerSprites = Directory
            .GetFiles("./assets/player/", "*.png")
            .Select(file => Bitmap.FromFile(file) as Bitmap)
            .ToList();
    }

    public static List<Bitmap> PlayerSprites = new();
}