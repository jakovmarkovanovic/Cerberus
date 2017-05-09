﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using BL.Servers.CoC.Core;
using BL.Servers.CoC.Extensions;
using BL.Servers.CoC.Logic;

namespace BL.Servers.CoC
{
    internal class Program
    {
        internal static Stopwatch Stopwatch = Stopwatch.StartNew();
        internal static void Main(string[] args)
        {
            Console.Title = $"BarbarianLand Clash Server - ©BarbarianLand ";
            //NativeCalls.SetWindowLong(NativeCalls.GetConsoleWindow(), -20, (int) NativeCalls.GetWindowLong(NativeCalls.GetConsoleWindow(), -20) ^ 0x80000);
            //NativeCalls.SetLayeredWindowAttributes(NativeCalls.GetConsoleWindow(), 0, 217, 0x2);

            Console.SetOut(new Prefixed());

            Console.ForegroundColor = Utils.ChooseRandomColor();

            Console.WriteLine(@"__________             ___.                .__                     .____                       .___");
            Console.WriteLine(@"\______   \_____ ______\_ |__ _____ _______|__|____    ____   _____|    |   _____    ____    __| _/");
            Console.WriteLine(@" |    |  _/\__  \\_  __ \ __ \\__  \\_  __ \  \__  \  /    \ /  ___/    |   \__  \  /    \  / __ | ");
            Console.WriteLine(@" |    |   \ / __ \|  | \/ \_\ \/ __ \|  | \/  |/ __ \|   |  \\___ \|    |___ / __ \|   |  \/ /_/ | ");
            Console.WriteLine(@" |______  /(____  /__|  |___  (____  /__|  |__(____  /___|  /____  >_______ (____  /___|  /\____ | ");
            Console.WriteLine(@"        \/      \/          \/     \/              \/     \/     \/        \/    \/     \/      \/  ");
            Console.WriteLine(@"                                                                           Developer Edition  ");

            Console.ResetColor();

#if DEBUG
            Console.ForegroundColor = Utils.ChooseRandomColor();
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Initialized Fluxcapacitor v7.79");
            Console.WriteLine("Loading hastables to run Fluxcapacitor v7.79 to connect to the International Space Station.");
            Console.WriteLine("Connecting to the custom MongoDB v5.2.0 server powered by nuclear energy (Uranium & Plutonium).");
            Console.WriteLine("Hacking into the NSA to get the CSV tables.");
            Console.WriteLine("Hacking into Supercell's Amazon Web Service to extract the latest keys.");
            Console.WriteLine("Server is ready to start sending nuclear warheads into space and handle Clash Of Clans connections.");
            Console.ResetColor();
            Console.WriteLine(Environment.NewLine);
#endif

            Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Name + @" is now starting..." + Environment.NewLine);
            Resources.Initialize();
            /*var s = Stopwatch.StartNew();
             int count = 4000;
             for (var i = 1; i < count; i++)
             {
                var a = Resources.Clans.New(i, Constants.Database);
                a.Members.Add(new Player(1) { Name = "Hi", Level = 1});
                 a.Members.Add(new Player(2) { Name = "Hi", Level = 1 });
                Resources.Clans.Save(a);
             }
             s.Stop();

             Console.WriteLine("Total second {0:N0}",  s.Elapsed.TotalSeconds);
             Console.WriteLine("{0:N0} queries per second", count / s.Elapsed.TotalSeconds);*/
            Console.WriteLine(@"-------------------------------------" + Environment.NewLine);

            while (true) ;

        }
    }
}