using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

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
    
    private Maze maze;
    private Space crrSpace;

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
    private Image floor12 = Image.FromFile("./assets/blocks/floor12.png");
    private Image floor13 = Image.FromFile("./assets/blocks/floor13.png");
    private Image floor14 = Image.FromFile("./assets/blocks/floor14.png");
    private Image floor15 = Image.FromFile("./assets/blocks/floor15.png");

    private Image wall = Image.FromFile("./assets/blocks/wall.png");

    private Image heart = Image.FromFile("./assets/objects/heart.png");
    private Image seed = Image.FromFile("./assets/objects/seed.png");

    private Image chestClosed = Image.FromFile("./assets/chests/flower1.png");
    private Image chestOpened = Image.FromFile("./assets/chests/flower2.png");

    public Image[] playerAnim = 
    {
        Bitmap.FromFile("./assets/player/1down.png"),
        Bitmap.FromFile("./assets/player/2down.png"),
        Bitmap.FromFile("./assets/player/3down.png"),
        Bitmap.FromFile("./assets/player/4up.png"),
        Bitmap.FromFile("./assets/player/5up.png"),
        Bitmap.FromFile("./assets/player/6up.png"),
        Bitmap.FromFile("./assets/player/7right.png"),
        Bitmap.FromFile("./assets/player/8right.png"),
        Bitmap.FromFile("./assets/player/9right.png"),
        Bitmap.FromFile("./assets/player/10left.png"),
        Bitmap.FromFile("./assets/player/11left.png"),
        Bitmap.FromFile("./assets/player/12left.png"),
    };

    public Image[] enemyAnim = 
    {
        Bitmap.FromFile("./assets/enemy/enemy1.png"),
        Bitmap.FromFile("./assets/enemy/enemy2.png")
    };

    public Image[] chestAnim = 
    {
        Bitmap.FromFile("./assets/chests/flower1.png"),
        Bitmap.FromFile("./assets/chests/flower2.png")
    };

    private float baseX = 400;
    private float baseY = 400;

    float lanternX = 960;
    float lanternY = 540;
    float radius = 1100;

    float playerX = 960;
    float playerY = 540;

    public Game()
    {
        maze = Maze.Prim(50, 50);
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
            G.InterpolationMode = InterpolationMode.NearestNeighbor;
            G.PixelOffsetMode = PixelOffsetMode.HighQuality;
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

                case Keys.Up:
                    maze.MoveUp();
                    break;

                case Keys.Left:
                    maze.MoveLeft();
                    break;

                case Keys.Down:
                    maze.MoveDown();
                    break;

                case Keys.Right:
                    maze.MoveRight();
                    break;
            }
        };

        KeyUp += (o, e) =>
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    maze.StopUp();
                    break;

                case Keys.Left:
                    maze.StopLeft();
                    break;

                case Keys.Down:
                    maze.StopDown();
                    break;

                case Keys.Right:
                    maze.StopRight();
                    break;
            }
        };
    }

    public void Tick()
    {
        G.Clear(Color.FromArgb(0xFF, 0x41, 0x98, 0x0A));
        Update();
        DrawMaze(400 + maze.Location.X, 400 + maze.Location.Y, crrSpace);
        DrawPlayer();
        DrawLantern(lanternX, lanternY, radius);
        DrawStats();
        this.Pb.Refresh();
        // DrawEnemies();
        TickCounter++;
    }

    public void Update()
    {
        maze.Move();
    }


    private void DrawMaze(float x, float y, Space space)
    {
        if (space == null)
            return;
        
        DrawWall(space, x, y);
    }

    private void DrawWall(Space space, float x, float y, List<Space> visited = null)
    {
        const float wallSize = 350;

        if (visited is null)
            visited = new();
        
        if (visited.Contains(space))
            return;
        visited.Add(space);

        var imgFloor = (space.Left, space.Top, space.Right, space.Bottom) switch {
            (null, null, null, _) => floor11,
            (null, null, _, null) => floor10,
            (null, _, null, null) => floor9,
            (_, null, null, null) => floor8,
            (null, null, _, _) => floor7,
            (null, _, _, null) => floor6,
            (_, _, null, null) => floor5,
            (_, null, null, _) => floor4,
            (_, null, _, null) => floor2,
            (null, _, null, _) => floor1,
            (_, null, _, _) => floor15,
            (null, _, _, _) => floor14,
            (_, _, _, null) => floor13,
            (_, _, null, _) => floor12,
            _ => floor3
        };

        G.DrawImage(imgFloor, x, y, wallSize, wallSize);

        if (space.Top == null)
            G.DrawImage(wall, x, y - 5, wallSize, 20);
        else DrawWall(space.Top, x, y - wallSize, visited);

        if (space.Bottom == null)
            G.DrawImage(wall, x, y + wallSize - 5, wallSize, 20);
        else DrawWall(space.Bottom, x, y + wallSize, visited);

        if (space.Left == null)
            G.DrawImage(wall, x - 5, y, 20, wallSize);
        else DrawWall(space.Left, x - wallSize, y, visited);

        if (space.Right == null)
            G.DrawImage(wall, x + wallSize - 5, y, 20, wallSize);
        else DrawWall(space.Right, x + wallSize, y, visited);

        // GameObject.CreateHitbox(this.X, this.Y, this.Width, this.Height);
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

    private void DrawPlayer()
    {
        G.DrawImage(playerAnim[0], 890, 470, 150, 150);
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

    private void DrawLantern(float x, float y, float radius)
    {
        float width = radius * 2;
        float height = radius * 2;

        RectangleF rect = new RectangleF(x - radius, y - radius, width, height);

        for (float dist = 0; dist <= this.Height; dist += 1)
        {
            var propDist = dist / this.Height;
            float alpha = 2 * propDist * propDist;

            int aChannel = 255 -
                (alpha < 0f ? 0 : 
                alpha > 1f ? 255 :
                (int)(255 * alpha));
            Color color = Color.FromArgb(aChannel, 0, 0, 0);
            RectangleF borderRect = new RectangleF(rect.X + dist, rect.Y + dist, rect.Width - 2 * dist, rect.Height - 2 * dist);
            G.DrawEllipse(new Pen(color, 4), borderRect);
        }
    }
}