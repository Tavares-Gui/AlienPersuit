using System;
using System.CodeDom;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

ApplicationConfiguration.Initialize();

Bitmap bmp = null;
Graphics g = null;

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

timer.Tick += (o, e) =>
{
    g.Clear(Color.White);
    // player.PlayerAndInfo(g, player, pb);
    g.DrawImage(Bitmap.FromFile("./assets/objects/heart.png"), 0, 0);
    g.DrawImage(Bitmap.FromFile("./assets/objects/seed.png"), 80, 0);
    
    pb.Refresh();
};

form.KeyDown += (o, e) =>
{
    switch (e.KeyCode)
    {
        case Keys.Escape:
            Application.Exit();
            break;
    }
};

// form.KeyUp += (o, e) =>
// {
//     switch (e.KeyCode)
//     {

//     }
// };

Application.Run(form);
