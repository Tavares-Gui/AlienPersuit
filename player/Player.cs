using System;
using System.Drawing;
using System.Security;
using System.Windows.Forms;

public class Player
{
    public int PlayerLife { get; set; } = 3;
    public int Seeds { get; set; } = 4;
    public int Tools { get; set; }
    public float Vx { get; set; }
    public float Vy { get; set; }
    public float Speed { get; set; } = 5;

    public void Draw(int playerLife, int seeds, int speed)
    {
        this.PlayerLife = playerLife;
        this.Seeds = seeds;
        this.Speed = speed;
    }
}
