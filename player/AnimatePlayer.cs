// using System;
// using System.IO;
// using System.Linq;
// using System.Drawing;
// using System.Collections.Generic;

// public class Resources
// {
//     private static Resources crr;
//     public static Resources Current => crr;
//     private Resources()
//     {
//         this.PlayerSprites = Directory.GetFiles("../assets/player/", "*.png")
//             .Select(file => Bitmap.FromFile(file) as Bitmap)
//             .ToList();

//     }

//     public List<Bitmap> PlayerSprites = new();

//     public static void New() => crr = new Resources();

// }