using System;
using System.CodeDom;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.Collections.Generic;

ApplicationConfiguration.Initialize();

Bitmap bmp = null;
Graphics g = null;

List<Image> walkUp = new List<Image>();
walkUp.Add(Image.FromFile("./assets/player/up2.png"));
walkUp.Add(Image.FromFile("./assets/player/defaultUp.png"));
walkUp.Add(Image.FromFile("./assets/player/up1.png"));
walkUp.Add(Image.FromFile("./assets/player/defaultUp.png"));

List<Image> walkDown = new List<Image>();
walkDown.Add(Image.FromFile("./assets/player/down2.png"));
walkDown.Add(Image.FromFile("./assets/player/downDefault.png"));
walkDown.Add(Image.FromFile("./assets/player/down1.png"));
walkDown.Add(Image.FromFile("./assets/player/downDefault.png"));

List<Image> walkLeft = new List<Image>();
walkLeft.Add(Image.FromFile("./assets/player/left3.png"));
walkLeft.Add(Image.FromFile("./assets/player/left2.png"));
walkLeft.Add(Image.FromFile("./assets/player/left1.png"));
walkLeft.Add(Image.FromFile("./assets/player/left2.png"));

List<Image> walkRight = new List<Image>();
walkRight.Add(Image.FromFile("./assets/player/right1.png"));
walkRight.Add(Image.FromFile("./assets/player/right2.png"));
walkRight.Add(Image.FromFile("./assets/player/right3.png"));
walkRight.Add(Image.FromFile("./assets/player/right2.png"));

Player player = new()
{
    Lifes = 3,
    Seeds = 3
};

var pb = new PictureBox {
    Dock = DockStyle.Fill,
};

var timer = new Timer {
    Interval = 20,
};

var form = new Form {
    WindowState = FormWindowState.Maximized,
    FormBorderStyle = FormBorderStyle.None,
    Controls = { pb }
};

// public void Draw(Graphics g)
// {
//     RectangleF rectBar = new RectangleF(
//         X - Size / 2, Y, Size, 40
//     );
//     g.FillRectangle(Brushes.Purple, rectBar);
//     g.DrawRectangle(Pens.Black, rectBar);
// }

form.Load += (o, e) =>
{
    bmp = new Bitmap(
        pb.Width, 
        pb.Height
    );
    g = Graphics.FromImage(bmp);
    g.Clear(Color.Black);
    pb.Image = bmp;
    timer.Start();
};

float x = 300, y = 300;
float vx = 0, vy = 0;

timer.Tick += (o, e) =>
{
    g.Clear(Color.White);
    // player.PlayerAndInfo(g, player, pb);
    g.DrawImage(Bitmap.FromFile("./assets/objects/heart.png"), 0, 0);
    g.DrawImage(Bitmap.FromFile("./assets/objects/seed.png"), 80, 0);
    g.DrawImage(Bitmap.FromFile("./assets/player/downDefault.png"), 0, 80);
    pb.Refresh();
    x += vx;
    y += vy;
    
    pb.Refresh();
};

form.KeyDown += (o, e) =>
{
    switch (e.KeyCode)
    {
        case Keys.Escape:
            Application.Exit();
            break;

        case Keys.Up:
            vy = -5;
            break;

        case Keys.Left:
            vx = -5;
            break;

        case Keys.Down:
            vy = 5;
            break;

        case Keys.Right:
            vx = 5;
            break;
    }
};

form.KeyUp += (o, e) =>
{
    switch (e.KeyCode)
    {
        case Keys.Up:
            vy = 0;
            break;

        case Keys.Left:
            vx = 0;
            break;

        case Keys.Down:
            vy = 0;
            break;

        case Keys.Right:
            vx = 0;
            break;
    }
};



Application.Run(form);
