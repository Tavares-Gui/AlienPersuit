using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

public class Game : Form
{
    public Graphics G { get; set; }
    public Bitmap Bmp { get; set; }
    public Timer Tmr { get; set; }
    public PictureBox Pb { get; set; }
    public int TickCounter { get; set; }
    public Player Player { get; set; }
    public Enemy Enemy { get; set; }


    public int X { get; set; }
    public int Y { get; set; }
    public Space Top { get; set; } = null;
    public Space Left { get; set; } = null;
    public Space Right { get; set; } = null;
    public Space Bottom { get; set; } = null;
    public bool Exit { get; set; } = false;

    private Maze maze;
    private Space crrSpace;

    private Image floor = Image.FromFile("./assets/blocks/floor.png");
    private Image wallUp = Image.FromFile("./assets/blocks/wallUp.png");
    private Image wallDown = Image.FromFile("./assets/blocks/wallDown.png");
    private Image wallRight = Image.FromFile("./assets/blocks/wallRight.png");
    private Image wallLeft = Image.FromFile("./assets/blocks/wallLeft.png");
    private Image heart = Image.FromFile("./assets/objects/heart.png");
    private Image seed = Image.FromFile("./assets/objects/seed.png");

    private float baseX = 200;
    private float baseY = 200;

    public Game()
    {
        maze = Maze.Prim(
            Random.Shared.Next(48),
            Random.Shared.Next(27)
        );
        crrSpace = maze.Spaces
            .OrderByDescending(s => Random.Shared.Next())
            .FirstOrDefault();

        var timer = new Timer
        {
            Interval = 20,
        };
        this.Tmr = timer;
        this.Player = new();
        this.Enemy = new();

        this.Pb = new()
        {
            Dock = DockStyle.Fill,
        };

        WindowState = FormWindowState.Maximized;
        FormBorderStyle = FormBorderStyle.None;

        this.Load += (o, e) =>
        {
            this.Bmp = new Bitmap(
                Pb.Width,
                Pb.Height
            );

            G = Graphics.FromImage(this.Bmp);
            Pb.Image = this.Bmp;
            timer.Start();
        };

        Controls.Add(Pb);
        timer.Tick += (o, e) => this.Tick();

        KeyDown += (o, e) =>
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Application.Exit();
                    break;
            }
        };

        // KeyUp += (o, e) =>
        // {
        //     switch (e.KeyCode)
        //     {

        //     }
        // };
    }

    public void Tick()
    {
        this.Pb.Refresh();
        DrawFloor();
        DrawMaze(baseX, baseY, crrSpace);
        DrawStats();
        TickCounter++;
    }

    private void DrawMaze(float x, float y, Space space)
    {
        if (space == null)
            return;

        DrawWall(space);

        if (space.Top != null)
            G.DrawImage(wallUp, x, y - wallUp.Height);
        if (space.Bottom != null)
            G.DrawImage(wallDown, x, y + wallDown.Height);
        if (space.Left != null)
            G.DrawImage(wallLeft, x - wallLeft.Width, y);
        if (space.Right != null)
            G.DrawImage(wallRight, x + wallRight.Width, y);
    }

    private void DrawFloor()
    {
        var cols = Bmp.Width / floor.Width;
        var lins = Bmp.Height / floor.Height;
        for (int i = -1; i < cols + 1; i++)
        {
            for (int j = -1; j < lins + 1; j++)
            {
                var x = i * floor.Width + baseX % floor.Height;
                var y = j * floor.Height + baseY % floor.Width;
                G.DrawImage(floor, new PointF(x, y));
            }
        }
    }

    private void DrawWall(Space space)
    {
        if (space.Top == null)
            G.DrawImage(wallUp, baseX, baseY - wallUp.Height);
        if (space.Bottom == null)
            G.DrawImage(wallDown, baseX, baseY + wallDown.Height);
        if (space.Left == null)
            G.DrawImage(wallLeft, baseX - wallLeft.Width, baseY);
        if (space.Right == null)
            G.DrawImage(wallRight, baseX + wallRight.Width, baseY);
    }

    private void DrawStats()
    {
        Color textColor = Color.White;
        SolidBrush textBrush = new(textColor);

        Font font = new("Arial", 12, FontStyle.Bold);

        G.DrawImage(heart, Pb.Width * 0.01f, Pb.Height * 0.01f);
        G.DrawImage(seed, Pb.Width * 0.06f, Pb.Height * 0.01f);
        G.DrawString(Player.PlayerLife.ToString(), font, textBrush, new PointF(Pb.Width * 0.05f, Pb.Height * 0.05f));
        G.DrawString(Player.Seeds.ToString(), font, textBrush, new PointF(Pb.Width * 0.10f, Pb.Height * 0.05f));
    }

    private void DrawEnemies()
    {

    }
}