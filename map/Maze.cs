using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Maze
{
    public Space Root { get; set; }
    public List<Space> Spaces { get; } = new();
    public PointF Location { get; set; } = new(0, 0);

    public short Ay { get; set; }
    public short Ax { get; set; }
    private DateTime dt = DateTime.Now;
    private float vx { get; set; }
    private float vy { get; set; }
    private float BaseAcceleration { get; set; } = 1_500;

    public void Reset()
    {
        foreach (var space in Spaces)
            space.Reset();
    }

    public static Maze Prim(int sx, int sy)
    {
        Maze maze = new Maze();
        var priority = new PriorityQueue<(int i, int j), byte>();
        byte[,] topgrid = new byte[sx, sy];
        byte[,] rightgrid = new byte[sx, sy];
        Space[,] vertices = new Space[sx, sy];
        int verticeCount = 0;

        for (int i = 0; i < sx; i++)
        {
            for (int j = 0; j < sy; j++)
            {
                topgrid[i, j] = (byte)Random.Shared.Next(255);
                rightgrid[i, j] = (byte)Random.Shared.Next(255);
            }
        }

        maze.Root = add(0, 0);

        while (priority.Count > 0)
        {
            var pos = priority.Dequeue();
            connect(pos.i, pos.j);
        }

        return maze;

        Space add(int i, int j)
        {
            if (vertices[i, j] is null)
            {
                var newSpace = new Space
                {
                    X = i,
                    Y = j
                };
                maze.Spaces.Add(newSpace);
                vertices[i, j] = newSpace;
                verticeCount++;
            }
            
            byte top = j == 0  || vertices[i, j - 1] is not null ? byte.MaxValue : topgrid[i, j],
                 bot = j == sy - 1 || vertices[i, j + 1] is not null ? byte.MaxValue : topgrid[i, j + 1],
                 rig = i == sx - 1 || vertices[i + 1, j] is not null ? byte.MaxValue : rightgrid[i, j],
                 lef = i == 0 || vertices[i - 1, j] is not null ? byte.MaxValue : rightgrid[i - 1, j];
            var min = byte.Min(
                byte.Min(top, bot),
                byte.Min(lef, rig)
            );
            if (min == byte.MaxValue)
                return vertices[i, j];

            priority.Enqueue((i, j), min);

            return vertices[i, j];
        }

        void connect(int i, int j)
        {   
            var crr = vertices[i, j];

            byte top = j == 0  || vertices[i, j - 1] is not null ? byte.MaxValue : topgrid[i, j],
                 bot = j == sy - 1 || vertices[i, j + 1] is not null ? byte.MaxValue : topgrid[i, j + 1],
                 rig = i == sx - 1 || vertices[i + 1, j] is not null ? byte.MaxValue : rightgrid[i, j],
                 lef = i == 0 || vertices[i - 1, j] is not null ? byte.MaxValue : rightgrid[i - 1, j];
            var min = byte.Min(
                byte.Min(top, bot),
                byte.Min(lef, rig)
            );
            if (min == byte.MaxValue)
                return;
            
            if (min == top)
            {
                var newSpace = add(i, j - 1);
                crr.Top = newSpace;
                newSpace.Bottom = crr;
            }
            else if (min == lef)
            {
                var newSpace = add(i - 1, j);
                crr.Left = newSpace;
                newSpace.Right = crr;
            }
            else if (min == rig)
            {
                var newSpace = add(i + 1, j);
                crr.Right = newSpace;
                newSpace.Left = crr;
            }
            else if (min == bot)
            {
                var newSpace = add(i, j + 1);
                crr.Bottom = newSpace;
                newSpace.Top = crr;
            }

            add(i, j);
        }
    }

    public void Move()
    {

        // oldVars

        var now = DateTime.Now;
        var time = now - dt;
        var sec = (float)time.TotalSeconds;
        dt = now;

        double magnitude = Math.Sqrt(Ax * Ax + Ay * Ay);

        if (magnitude != 0)
        {
            vx += (float)(Ax / magnitude) * BaseAcceleration * sec;
            vy += (float)(Ay / magnitude) * BaseAcceleration * sec;
        }

        this.Location = new(Location.X + vx * sec , Location.Y + vy * sec);

        // resistencia pra parada
        vx *= MathF.Pow(0.00001f, sec);
        vy *= MathF.Pow(0.00001f, sec);

        const int max = 60;
        if (vx > max)
            vx = max;
        else if (vx < -max)
            vx = -max;

        if (vy > max)
            vy = max;
        else if (vy < -max)
            vy = -max;

        //Verificacao de Colisao

        //acao pos colisao
    }

    public void MoveUp () => Ay = 1;
    public void MoveDown () => Ay = -1;
    public void MoveRight () => Ax = -1;
    public void MoveLeft () => Ax = 1;

    public void StopUp () => Ay = 0;
    public void StopDown () => Ay = 0;
    public void StopRight () => Ax = 0;
    public void StopLeft () => Ax = 0;
}


public class Space
{
    public int X { get; set; }
    public int Y { get; set; }
    public Space Top { get; set; } = null;
    public Space Left { get; set; } = null;
    public Space Right { get; set; } = null;
    public Space Bottom { get; set; } = null;
    public bool Visited { get; set; } = false;
    public bool IsSolution { get; set; } = false;
    public bool Exit { get; set; } = false;

    public void Reset()
    {
        IsSolution = false;
        Visited = false;
    }
}
