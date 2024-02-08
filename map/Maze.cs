using System;
using System.Drawing;
using System.Collections.Generic;
using System.Dynamic;
using System.Windows.Forms;
using System.Drawing.Text;
using System.CodeDom;

public class Maze
{
    const float wallSize = 350;
    public Space Root { get; set; }
    public List<Space> Spaces { get; } = new();
    public PointF Location { get; set; } = new(0, 0);
    public PointF InitialLocation { get; set; } = new(0, 0);
    public short Ay { get; set; }
    public short Ax { get; set; }
    private DateTime dt = DateTime.Now;
    private float vx { get; set; }
    private float vy { get; set; }
    private float BaseAcceleration { get; set; } = 5_000;
    private float xPortal { get; set; }
    private float yPortal { get; set; }
    private static int StashSx { get; set; }
    private static int StashSy { get; set; }
    
    private Space lefttop = null;

    Portal portal = new();

    public void Reset()
    {
        foreach (var space in Spaces)
            space.Reset();
    }

    public static Maze Prim(int sx, int sy, float wid, float hei)
    {
        StashSx = sx;
        StashSy = sy;

        Portal.PortalCreated = false;
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
                topgrid[i, j] = (byte)GlobalSeed.Current.Random.Next(255);
                rightgrid[i, j] = (byte)GlobalSeed.Current.Random.Next(255);
            }
        }

        maze.Root = add(0, 0);
        maze.Location = new PointF(
            wid / 2 - wallSize / 2,
            hei / 2 - wallSize / 2
        );
        maze.InitialLocation = new PointF(
            wid / 2 - wallSize / 2,
            hei / 2 - wallSize / 2
        );

        while (priority.Count > 0)
        {
            var pos = priority.Dequeue();
            connect(pos.i, pos.j);
        }

        maze.lefttop = vertices[0, 0];
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

            byte top = j == 0 || vertices[i, j - 1] is not null ? byte.MaxValue : topgrid[i, j],
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

            byte top = j == 0 || vertices[i, j - 1] is not null ? byte.MaxValue : topgrid[i, j],
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

    public void Move(RectangleF player, Space crrSpace)
    {
        // oldVars
        var oldLocation = this.Location;

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

        this.Location = new(Location.X + vx * sec, Location.Y + vy * sec);

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

        if (HasWall(player, Location.X, Location.Y, crrSpace))
            Location = oldLocation;

        if (portal.HasPlayer(player, crrSpace))
        {
            if (OnExit is not null)
                OnExit();
        }
    }

    public void MoveUp() => Ay = 1;
    public void MoveDown() => Ay = -1;
    public void MoveRight() => Ax = -1;
    public void MoveLeft() => Ax = 1;


    public void StopY_axis() => Ay = 0;
    public void StopX_axis() => Ax = 0;

    public void StopUp() => Ay = 0;
    public void StopDown() => Ay = 0;
    public void StopRight() => Ax = 0;
    public void StopLeft() => Ax = 0;
    

    public bool HasWall(RectangleF player, float x, float y, Space crrSpace)
        => hasWall(player, x, y, crrSpace);

    private bool hasWall(RectangleF player, float x, float y, Space space, List<Space> visited = null)
    {
        visited ??= new();

        if (visited.Contains(space))
            return false;
        visited.Add(space);

        RectangleF playerHitbox = new RectangleF(player.X, player.Y, Player.SizeX, Player.SizeY);

        if (space.Top is not null && hasWall(playerHitbox, x, y - wallSize, space.Top, visited))
            return true;
        else if (space.Top is null && playerHitbox.IntersectsWith(new RectangleF(x, y - 5, wallSize, 5)))
            return true;

        if (space.Bottom is not null && hasWall(playerHitbox, x, y + wallSize, space.Bottom, visited))
            return true;
        else if (space.Bottom is null && playerHitbox.IntersectsWith(new RectangleF(x, y + wallSize - 5, wallSize, 5)))
            return true;

        if (space.Left is not null && hasWall(playerHitbox, x - wallSize, y, space.Left, visited))
            return true;
        else if (space.Left is null && playerHitbox.IntersectsWith(new RectangleF(x - 5, y, 5, wallSize)))
            return true;

        if (space.Right is not null && hasWall(playerHitbox, x + wallSize, y, space.Right, visited))
            return true;
        else if (space.Right is null && playerHitbox.IntersectsWith(new RectangleF(x + wallSize - 5, y, 5, wallSize)))
            return true;

        return false;
    }

    public void Draw(Graphics g, Space space)
    {
        if (space == null)
            return;

        DrawWall(g, space, Location.X, Location.Y);

        if (!Portal.PortalCreated)
        {
            xPortal = wallSize / 2 + wallSize * GlobalSeed.Current.Random.Next(0, StashSx);
            yPortal = wallSize / 2 + wallSize * GlobalSeed.Current.Random.Next(0, StashSy);
            Portal.PortalCreated = true;
        }

        var crr = GetCurrentSpace(space, Location.X, Location.Y);
        if (crr is null)
            return;
        var leftPos = GetLeftPoint(crr, Location.X, Location.Y);
        Portal.Draw(g, xPortal + leftPos.X, yPortal + leftPos.Y);
    }

    private void DrawWall(Graphics g, Space space, float x, float y, List<Space> visited = null)
    {
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
        g.DrawImage(imgFloor, x, y, wallSize, wallSize);

        if (space.Top == null)
            g.DrawImage(Images.wall[0], x, y - 5, wallSize, 20);
        else
        {
            DrawWall(g, space.Top, x, y - wallSize, visited);
        }

        if (space.Bottom == null)
            g.DrawImage(Images.wall[0], x, y + wallSize - 5, wallSize, 20);
        else
        {
            DrawWall(g, space.Bottom, x, y + wallSize, visited);
        }

        if (space.Left == null)
            g.DrawImage(Images.wall[0], x - 5, y, 20, wallSize);
        else
        {
            DrawWall(g, space.Left, x - wallSize, y, visited);
        }

        if (space.Right == null)
            g.DrawImage(Images.wall[0], x + wallSize - 5, y, 20, wallSize);
        else
        {
            DrawWall(g, space.Right, x + wallSize, y, visited);
        }
    }

    public PointF GetLeftPoint(Space crr, float crrX, float crrY, List<Space> visited = null)
    {
        visited ??= new();
        if (crr == lefttop)
            return new PointF(crrX, crrY);

        if (visited.Contains(crr))
            return Point.Empty;
        visited.Add(crr);
        
        if (crr.Top is not null)
        {
            var pt = GetLeftPoint(crr.Top, crrX, crrY - wallSize, visited);
            if (pt != Point.Empty)
                return pt;
        }

        if (crr.Left is not null)
        {
            var pt = GetLeftPoint(crr.Left, crrX - wallSize, crrY, visited);
            if (pt != Point.Empty)
                return pt;
        }

        if (crr.Bottom is not null)
        {
            var pt = GetLeftPoint(crr.Bottom, crrX, crrY + wallSize, visited);
            if (pt != Point.Empty)
                return pt;
        }

        if (crr.Right is not null)
        {
            var pt = GetLeftPoint(crr.Right, crrX + wallSize, crrY, visited);
            if (pt != Point.Empty)
                return pt;
        }

        return Point.Empty;
    }

    public Space GetCurrentSpace(Space crr, float crrX, float crrY, List<Space> visited = null)
    {
        visited ??= new();

        var spaceRect = new RectangleF(crrX, crrY, wallSize, wallSize);
        var playerPos = new PointF(Location.X + wallSize / 2, Location.Y + wallSize / 2);
        if (spaceRect.Contains(playerPos))
            return crr;

        if (visited.Contains(crr))
            return null;
        visited.Add(crr);
        
        if (crr.Top is not null)
        {
            var space = GetCurrentSpace(crr.Top, crrX, crrY - wallSize, visited);
            if (space is not null)
                return space;
        }

        if (crr.Left is not null)
        {
            var space = GetCurrentSpace(crr.Left, crrX - wallSize, crrY, visited);
            if (space is not null)
                return space;
        }

        if (crr.Bottom is not null)
        {
            var space = GetCurrentSpace(crr.Bottom, crrX, crrY + wallSize, visited);
            if (space is not null)
                return space;
        }

        if (crr.Right is not null)
        {
            var space = GetCurrentSpace(crr.Right, crrX + wallSize, crrY, visited);
            if (space is not null)
                return space;
        }

        return null;
    }

    public event Action OnExit;
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
