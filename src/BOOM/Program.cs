using System.Threading;

namespace CRepublic.Boom
{
    using System;
    using System.Reflection;
    using System.Diagnostics;
    using CRepublic.Boom.Core;
    using CRepublic.Boom.Core.Consoles;
    using CRepublic.Boom.Extensions;

    internal class Program
    {
        internal static Stopwatch Stopwatch = Stopwatch.StartNew();

        internal static void Main()
        {
            Console.Title = $"Clasher Republic Boom Server - �Clashers Repbulic";
            //NativeCalls.SetWindowLong(NativeCalls.GetConsoleWindow(), -20, (int) NativeCalls.GetWindowLong(NativeCalls.GetConsoleWindow(), -20) ^ 0x80000);
            //NativeCalls.SetLayeredWindowAttributes(NativeCalls.GetConsoleWindow(), 0, 217, 0x2);

            Console.SetOut(new Prefixed());

            Console.ForegroundColor = Utils.ChooseRandomColor();

            Console.WriteLine(@"_________ .__                .__                          __________                   ___.   .__  .__        ");
            Console.WriteLine(@"\_   ___ \|  | _____    _____|  |__   ___________  ______ \______   \ ____ ______  __ _\_ |__ |  | |__| ____  ");
            Console.WriteLine(@"/    \  \/|  | \__  \  /  ___/  |  \_/ __ \_  __ \/  ___/  |       _// __ \\____ \|  |  \ __ \|  | |  |/ ___\ ");
            Console.WriteLine(@"\     \___|  |__/ __ \_\___ \|   Y  \  ___/|  | \/\___ \   |    |   \  ___/|  |_> >  |  / \_\ \  |_|  \  \___ ");
            Console.WriteLine(@" \______  /____(____  /____  >___|  /\___  >__|  /____  >  |____|_  /\___  >   __/|____/|___  /____/__|\___  >");
            Console.WriteLine(@"        \/          \/     \/     \/     \/           \/          \/     \/|__|             \/             \/ ");
            Console.WriteLine(@"                                                                                        Developer Edition  ");

            Console.ResetColor();

            Console.WriteLine(@"Clashers Repbulic's programs are protected by our policies, available on our main website.");
            Console.WriteLine(@"Clashers Repbulic's programs are under the 'CC Non-Commercial-NoDerivs 3.0 Unported' license.");
            Console.WriteLine(@"Clashers Repbulic is NOT affiliated to 'Supercell Oy'.");
            Console.WriteLine(@"Clashers Repbulic does NOT own 'Clash of Clans', 'Boom Beach', 'Clash Royale'.");

            Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Name + @" is now starting..." + Environment.NewLine);

            Resources.Initialize();
            Console.WriteLine(@"-------------------------------------" + Environment.NewLine);

            Thread.Sleep(-1);
        }
    }
}
