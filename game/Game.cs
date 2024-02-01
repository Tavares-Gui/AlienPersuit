using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class Game : Form
{
    public Graphics G { get; set; }
    public Bitmap Bmp { get; set; }
    public Timer Tmr { get; set; }
    public static PictureBox Pb { get; set; }

    private Lantern lantern = new();
    private Player player = new();
    private Maze maze = new();
    private Space crrSpace;
    bool loaded = false;

    public void Reset()
    {
        this.Controls.Clear();
        if (this.Tmr is not null)
            this.Tmr.Stop();

        maze = Maze.Prim(50, 50);
        crrSpace = maze.Spaces
            .OrderByDescending(s => GlobalSeed.Current.Random.Next())
            .FirstOrDefault();

        var timer = new Timer
        {
            Interval = 20,
        };
        this.Tmr = timer;
        this.player = new();

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
            G.InterpolationMode = InterpolationMode.NearestNeighbor;
            G.PixelOffsetMode = PixelOffsetMode.HighQuality;
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
                G.InterpolationMode = InterpolationMode.NearestNeighbor;
                G.PixelOffsetMode = PixelOffsetMode.HighQuality;
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
        player.Draw(G, Pb);
        lantern.Draw(G, Pb);
        player.DrawStats(G, Pb);
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
}
