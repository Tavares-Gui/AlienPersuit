// using System.Drawing;
// using System.Windows.Forms;

// public abstract class GameObject
// {
//     public float X { get; set; }
//     public float Y { get; set; }
//     public float Width { get; set; }
//     public float Height { get; set; }
//     public float New_X { get; set; }
//     public float New_Y { get; set; }
//     public Image Sprite { get; set; }

//     public GameObject(int x, int y, string sprite)
//     {
//         this.X = x;
//         this.Y = y;
//         setImage(sprite);
//         this.Width = this.Sprite.Width;
//         this.Height = this.Sprite.Height;
//     }
//     public GameObject(int x, int y, float width, float height)
//     {
//         this.X = x;
//         this.Y = y;
//         this.Width = width;
//         this.Height = height;
//     }

//     public virtual void Render(Graphics g, PictureBox pb) { }

//     public void setImage(string path)
//     {
//         this.Sprite = Bitmap.FromFile(path);
//     }
// }