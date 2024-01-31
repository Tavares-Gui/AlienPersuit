using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Configuration;

public class Game : Form
{
    public Graphics G { get; set; }
    public Bitmap Bmp { get; set; }
    public Timer Tmr { get; set; }
    public static PictureBox Pb { get; set; }
    public Player Player { get; set; }
    public Enemy Enemy { get; set; }
    public Chest Chest { get; set; }
    public int Index { get; set; } = 0;
    Random randPosition = new Random();
    public Pen pen { get; set; }
    public bool chestCreated { get; set; } = false;
    public bool enemiesCreated { get; set; } = false;

    private Maze maze;
    private Space crrSpace;

    private float baseX = 400;
    private float baseY = 400;

    float lanternX = 960;
    float lanternY = 540;
    float radius = 1100;

    float playerX = 960;
    float playerY = 540;

    int spawnsCounter = 0;

    public Game()
    {
        // Collisions.New();
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

        Pb = new()
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
            // G.CompositingQuality = CompositingQuality.AssumeLinear;
            G.InterpolationMode = InterpolationMode.NearestNeighbor;
            G.PixelOffsetMode = PixelOffsetMode.HighQuality;
            // G.SmoothingMode = SmoothingMode.HighSpeed;
            Random randNum = new Random();
            Point chestPosition = new(randNum.Next(Pb.Width), randNum.Next(Pb.Height));
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
        G.Clear(Color.Black);
        Update();
        DrawMaze(400 + maze.Location.X, 400 + maze.Location.Y, crrSpace);
        DrawPlayer();
        DrawEnemies();
        DrawLantern();
        DrawStats();
        Pb.Refresh();
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

        G.DrawRectangle(Pens.Red, new RectangleF(927 + 80 / 2, 462 + 75 / 2, 77, 95));
        if (visited is null)
            visited = new();

        if (visited.Contains(space))
            return;
        visited.Add(space);

        var imgFloor = (space.Left, space.Top, space.Right, space.Bottom) switch
        {
            (null, null, null, _) => Images.floors[10],
            (null, null, _, null) => Images.floors[9],
            (null, _, null, null) => Images.floors[8],
            (_, null, null, null) => Images.floors[7],
            (null, null, _, _) => Images.floors[6],
            (null, _, _, null) => Images.floors[5],
            (_, _, null, null) => Images.floors[4],
            (_, null, null, _) => Images.floors[3],
            (_, null, _, null) => Images.floors[1],
            (null, _, null, _) => Images.floors[0],
            (_, null, _, _) => Images.floors[14],
            (null, _, _, _) => Images.floors[13],
            (_, _, _, null) => Images.floors[12],
            (_, _, null, _) => Images.floors[11],
            _ => Images.floors[2]
        };
        G.DrawImage(imgFloor, x, y, wallSize, wallSize);

        DrawChests(space, x, y);

        if (space.Top == null)
        {
            G.DrawImage(Images.wall[0], x, y - 5, wallSize, 20);
            G.DrawRectangle(Pens.Red, new RectangleF(x, y - 5, wallSize, 20));

        }
        else
        {
            DrawWall(space.Top, x, y - wallSize, visited);
            G.DrawRectangle(Pens.Red, new RectangleF(x, y - 5, wallSize, 20));
        }

        if (space.Bottom == null)
        {
            G.DrawImage(Images.wall[0], x, y + wallSize - 5, wallSize, 20);
            G.DrawRectangle(Pens.Red, new RectangleF(x, y + wallSize - 5, wallSize, 20));
        }
        else
        {
            DrawWall(space.Bottom, x, y + wallSize, visited);
            G.DrawRectangle(Pens.Red, new RectangleF(x, y + wallSize - 5, wallSize, 20));
        }

        if (space.Left == null)
        {
            G.DrawImage(Images.wall[0], x - 5, y, 20, wallSize);
            G.DrawRectangle(Pens.Red, new RectangleF(x - 5, y, 20, wallSize));
        }
        else
        {
            DrawWall(space.Left, x - wallSize, y, visited);
            G.DrawRectangle(Pens.Red, new RectangleF(x - 5, y, 20, wallSize));
        }

        if (space.Right == null)
        {
            G.DrawImage(Images.wall[0], x + wallSize - 5, y, 20, wallSize);
            G.DrawRectangle(Pens.Red, new RectangleF(x + wallSize - 5, y, 20, wallSize));
        }
        else
        {
            DrawWall(space.Right, x + wallSize, y, visited);
            G.DrawRectangle(Pens.Red, new RectangleF(x + wallSize - 5, y, 20, wallSize));
        }
    }

    private void DrawStats()
    {
        Color textColor = Color.White;
        SolidBrush textBrush = new(textColor);

        Font font = new("Arial", 12, FontStyle.Bold);

        G.DrawImage(Images.stats[0], Pb.Width * 0.01f, Pb.Height * 0.01f);
        G.DrawImage(Images.stats[1], Pb.Width * 0.06f, Pb.Height * 0.01f);
        G.DrawString(Player.PlayerLife.ToString(), font, textBrush, new PointF(Pb.Width * 0.05f, Pb.Height * 0.05f));
        G.DrawString(Player.Seeds.ToString(), font, textBrush, new PointF(Pb.Width * 0.10f, Pb.Height * 0.05f));
    }

    private void DrawPlayer()
    {
        G.DrawImage(Player.playerAnim[0], 930, 470, 150, 150);
    }

    private void DrawEnemies()
    {
        foreach (Enemy enemy in Enemy.Enemies)
        {
            G.DrawImage(enemy.img[0], enemy.X + maze.Location.X, enemy.Y + maze.Location.Y, enemy.Size, enemy.Size);
        }
    }

    private void DrawChests(Space space, float x, float y)
    {
        return;
        foreach (Chest chest in Chest.Chests)
        {
            if (chest.img.Count > 0)
            {
                x++;
                y++;

                if (
                    space.Left == null && space.Top == null && space.Right == null ||
                    space.Left == null && space.Top == null && space.Bottom == null ||
                    space.Left == null && space.Right == null && space.Bottom == null ||
                    space.Top == null && space.Right == null && space.Bottom == null
                )
                {
                    G.DrawImage(chest.img[0], new RectangleF(x, y, (int)chest.Size, (int)chest.Size));
                }
            }
        }
    }

    private void DrawLantern()
    {
        const float min = .5f;
        const float max = .9f;

        const int erro = 40; // ????
        GraphicsPath path = new GraphicsPath();

        float radius = MathF.Sqrt(Pb.Width * Pb.Width + Pb.Height * Pb.Height) / 2;

        path.AddEllipse(
            Pb.Width / 2 - radius - erro,
            Pb.Height / 2 - radius,
            2 * radius + 2 * erro, 2 * radius + 2 * erro
        );

        ColorBlend blend = new ColorBlend();
        blend.Colors = new Color[] {
            Color.FromArgb(255, 0, 0, 0),
            Color.FromArgb(255, 0, 0, 0),
            Color.FromArgb(0, 0, 0, 0),
            Color.FromArgb(0, 0, 0, 0),
        };
        blend.Positions = new float[] {
            0f,
            min,
            max,
            1f
        };


        var brush = new PathGradientBrush(path)
        {
            InterpolationColors = blend
        };

        G.FillRectangle(brush, new Rectangle(0, 0, Pb.Width, Pb.Height));
    }
}
