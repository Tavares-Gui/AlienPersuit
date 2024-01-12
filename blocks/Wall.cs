// using System;
// using System.Drawing;
// using System.Windows.Forms;
// using System.Collections.Generic;

// public class Wall
// {
//     public float Size { get; set; }
//     public float X { get; set; }
//     public float Y { get; set; }

//     public void Draw(Graphics g)
//     {
//         RectangleF rectBar = new RectangleF(
//             X - Size / 2, Y, Size, 40
//         );
//         g.FillRectangle(Brushes.Purple, rectBar);
//         g.DrawRectangle(Pens.Black, rectBar);
//     }
// }