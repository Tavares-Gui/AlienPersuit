using System.Threading;
using System.Windows.Forms;

Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
Thread.CurrentThread.SetApartmentState(ApartmentState.STA);

ApplicationConfiguration.Initialize();

if (args.Length > 0)
    GlobalSeed.Reset(int.Parse(args[0]));

Application.Run(new Game());