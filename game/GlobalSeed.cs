using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Threading.Tasks;

public class GlobalSeed
{
    private static GlobalSeed current = new GlobalSeed();
    public static GlobalSeed Current => current;

    public static void Reset(int seed)
        => current = new GlobalSeed(seed);

    private Random random;
    public GlobalSeed()
    {
        Random rand = new Random(
            DateTime.Now.Millisecond +
            DateTime.Now.Second * 1000
        );
        this.Seed = rand.Next();
        this.random = new Random(Seed);
    }
    public GlobalSeed(int seed)
    {
        this.Seed = seed;
        this.random = new Random(Seed);
    }
    public int Seed { get; private set; }
    public Random Random => this.random;
}