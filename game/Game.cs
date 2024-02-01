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
    bool loaded = false;

    public void Reset()
    {
        this.Controls.Clear();
        if (this.Tmr is not null)
            this.Tmr.Stop();
        
        // Collisions.New();
        maze = Maze.Prim(50, 50);
        crrSpace = maze.Spaces
            .OrderByDescending(s => GlobalSeed.Current.Random.Next())
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

        if (loaded)
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
            Point chestPosition = new(
                GlobalSeed.Current.Random.Next(Pb.Width), 
                GlobalSeed.Current.Random.Next(Pb.Height)
            );
            timer.Start();
        }
        else 
        {
            this.Load += (o, e) =>
            {
                loaded = true;
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
                Point chestPosition = new(
                    GlobalSeed.Current.Random.Next(Pb.Width), 
                    GlobalSeed.Current.Random.Next(Pb.Height)
                );
                timer.Start();
            };
        }

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
                
                case Keys.C:
                    Clipboard.SetText(GlobalSeed.Current.Seed.ToString());
                    break;
                
                case Keys.V:
                    GlobalSeed.Reset(int.Parse(Clipboard.GetText()));
                    Reset();
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

    public Game()
    {
        Reset();
    }

    public void Tick()
    {
        G.Clear(Color.Black);
        Update();
        maze.Draw(G, crrSpace);
        DrawPlayer();
        // DrawEnemies();
        DrawLantern();
        DrawStats();

        G.DrawString(
            $"seed: {GlobalSeed.Current.Seed}. press C to copy seed.",
            SystemFonts.MenuFont,
            Brushes.White,
            new PointF(20, Pb.Height - 20)
        );
        Pb.Refresh();
    }

    public new void Update()
    {
        maze.Move(new RectangleF( 
            Pb.Width / 2 - 75, 
            Pb.Height / 2 -75 ,
            150, 150), crrSpace
        );
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
        G.DrawImage(Player.playerAnim[0], 
            Pb.Width / 2 - 75, 
            Pb.Height / 2 -75 ,
            150, 150
        );
    }

    // private void DrawEnemies()
    // {
    //     foreach (Enemy enemy in Enemy.Enemies)
    //     {
    //         G.DrawImage(enemy.img[0], Enemy.SetPosition(space), Enemy.SetPosition(space), enemy.Size, enemy.Size);
    //     }
    // }

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

        const int erro = 0; // ????
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
