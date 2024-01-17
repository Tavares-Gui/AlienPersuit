using System;
using System.Collections.Generic;

public class Maze
{
    public Space Root { get; set; }
    public List<Space> Spaces { get; } = new();
    public void Reset()
    {
        foreach (var space in Spaces)
            space.Reset();
    }

    public static Maze Prim(int sx, int sy)
    {
        Maze maze = new Maze();
        var priority = new PriorityQueue<(int i, int j), byte>();
        byte[,] topgrid = new byte[48, 27];
        byte[,] rightgrid = new byte[48, 27];
        Space[,] vertices = new Space[48, 27];
        int verticeCount = 0;

        for (int i = 0; i < 48; i++)
        {
            for (int j = 0; j < 27; j++)
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
                    Y = j,
                    Exit = sx == i && sy == j
                };
                maze.Spaces.Add(newSpace);
                vertices[i, j] = newSpace;
                verticeCount++;
            }
            
            byte top = j == 0  || vertices[i, j - 1] is not null ? byte.MaxValue : topgrid[i, j],
                 bot = j == 26 || vertices[i, j + 1] is not null ? byte.MaxValue : topgrid[i, j + 1],
                 rig = i == 47 || vertices[i + 1, j] is not null ? byte.MaxValue : rightgrid[i, j],
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
                 bot = j == 26 || vertices[i, j + 1] is not null ? byte.MaxValue : topgrid[i, j + 1],
                 rig = i == 47 || vertices[i + 1, j] is not null ? byte.MaxValue : rightgrid[i, j],
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
