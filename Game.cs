using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Net.Http.Headers;

public class Game : Form
{
    public Graphics G { get; set; }
    public Bitmap Bmp { get; set; }
    public Timer Tmr { get; set; }
    public PictureBox Pb { get; set; }
    public int TickCounter { get; set; }
    public Player Player { get; set; }
    public Enemy Enemy { get; set; }
    public Chest Chest { get; set; }
    public int Index { get; set; } = 0;
      private Random random = new Random();

    public int X { get; set; }
    public int Y { get; set; }
    public Space Top { get; set; } = null;
    public Space Left { get; set; } = null;
    public Space Right { get; set; } = null;
    public Space Bottom { get; set; } = null;
    public bool Exit { get; set; } = false;
    public float CurrentX { get; set; }
    public float CurrentY { get; set; }
    public float TargetX { get; set; } = 0;
    public float TargetY { get; set; } = 0;

    private Maze maze;
    private Space crrSpace;

    private Image floor = Image.FromFile("./assets/blocks/floor.png");
    private Image floor1 = Image.FromFile("./assets/blocks/floor1.png");
    private Image floor2 = Image.FromFile("./assets/blocks/floor2.png");
    private Image floor3 = Image.FromFile("./assets/blocks/floor3.png");
    private Image floor4 = Image.FromFile("./assets/blocks/floor4.png");
    private Image floor5 = Image.FromFile("./assets/blocks/floor5.png");
    private Image floor6 = Image.FromFile("./assets/blocks/floor6.png");
    private Image floor7 = Image.FromFile("./assets/blocks/floor7.png");
    private Image floor8 = Image.FromFile("./assets/blocks/floor8.png");
    private Image floor9 = Image.FromFile("./assets/blocks/floor9.png");
    private Image floor10 = Image.FromFile("./assets/blocks/floor10.png");
    private Image floor11 = Image.FromFile("./assets/blocks/floor11.png");

    private Image wallVertical = Image.FromFile("./assets/blocks/wallVertical.png");
    private Image wallHorizontal = Image.FromFile("./assets/blocks/wallHorizontal.png");

    private Image heart = Image.FromFile("./assets/objects/heart.png");
    private Image seed = Image.FromFile("./assets/objects/seed.png");

    private Image chestClosed = Image.FromFile("./assets/objects/chestClosed.png");
    private Image chestOpened = Image.FromFile("./assets/objects/chestOpened.png");

    public Image[] enemyAnim = 
    {
        Bitmap.FromFile("./assets/enemy/enemy1.png"),
        Bitmap.FromFile("./assets/enemy/enemy2.png")
    };

    public Image[] chestAnim = 
    {
        Bitmap.FromFile("./assets/objects/chestClosed.png"),
        Bitmap.FromFile("./assets/objects/chestOpened.png")
    };

    private float baseX = 200;
    private float baseY = 200;

    public Game()
    {
        maze = Maze.Prim(3, 3);
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
        this.Chest = new();

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
        G.Clear(Color.White);
        DrawFloor();
        DrawMaze(baseX, baseY, crrSpace);
        this.Pb.Refresh();
        // DrawChest();
        // DrawEnemies();
        // DrawStats();
        TickCounter++;
    }

    private void DrawMaze(float x, float y, Space space)
    {
        if (space == null)
            return;

        DrawWall(space, x, y);
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

    //need to fix the wall
    private void DrawWall(Space space, float x, float y, List<Space> visited = null)
    {
        const int wid = 100;
        const int hei = 100;

        if (visited is null)
            visited = new();
        
        if (visited.Contains(space))
            return;
        visited.Add(space);

        if (space.Top == null)
            G.DrawImage(wallHorizontal, x, y - 5, wid, 10);
        else DrawWall(space.Top, x, y - hei, visited);

        if (space.Bottom == null)
            G.DrawImage(wallHorizontal, x, y - 5, wid, 10);
        else DrawWall(space.Bottom, x, y + hei, visited);

        if (space.Left == null)
            G.DrawImage(wallHorizontal, x - 5, y, 10, hei);
        else DrawWall(space.Left, x - wid, y, visited);

        if (space.Right == null)
            G.DrawImage(wallHorizontal, x - 5, y, 10, hei);
        else DrawWall(space.Right, x + wid, y, visited);
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
        const int speedAnimEnemy = 6;

        if (Enemy.EnemyLife > 0)
        {
            if (Index < speedAnimEnemy)
            {
                G.DrawImage(enemyAnim[0], 500, 500);
                Index++;
            }
            else
            {
                G.DrawImage(enemyAnim[1], 500, 500);
                Index++;
                if (Index > 2 * speedAnimEnemy)
                    Index = 0;
            }
        }
    }

    private void DrawChest()
    {
        if (Chest.Open == true)
            G.DrawImage(chestAnim[1], 100, 100);
        else G.DrawImage(chestAnim[0], 100, 100);
    }

    private void DrawLantern(){ }
}