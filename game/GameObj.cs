using System.Drawing;

public abstract class GameObj
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public Image Sprite { get; set; }

    public GameObj(float x, float y, string sprite)
    {
        this.X = x;
        this.Y = y;
        setImage(sprite);
        this.Width = this.Sprite.Width;
        this.Height = this.Sprite.Height;
    }

    public void setImage(string path)
        => this.Sprite = Bitmap.FromFile(path);
}