﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Servers.CoC.Core.Networking;
using BL.Servers.CoC.Extensions;
using BL.Servers.CoC.Logic;
using BL.Servers.CoC.Logic.Enums;
using BL.Servers.CoC.Logic.Structure;
using BL.Servers.CoC.Packets.Messages.Server.Errors;
using Timer = System.Timers.Timer;

namespace BL.Servers.CoC.Core
{
    internal class Timers
    {
        internal readonly Dictionary<int, Timer> LTimers = new Dictionary<int, Timer>();

        internal Timers()
        {
            this.Save();
            this.DeadSockets();
            this.Random();
            this.Run();
        }
        internal void Maintenance(int durations)
        {
            foreach (var _Device in Resources.Players.Values.ToList())
            {
                if (_Device.Client != null)
                {
                    new Server_Shutdown(_Device.Client).Send();
                }
            }

            Timer Timer = new Timer
            {
                Interval = TimeSpan.FromMinutes(5).TotalMilliseconds,
                AutoReset = false,
            };

            Timer.Elapsed += (_Sender, _Args) =>
            {
                foreach (var _Device in Resources.Devices.Values.ToList())
                {
                    Resources.Gateway.Disconnect(_Device.Token.Args);
                }

                Constants.Maintenance = new Maintenance_Timer();
                Constants.Maintenance.StartTimer(DateTime.Now, (int)TimeSpan.FromMinutes(durations).TotalSeconds);

                Console.WriteLine("# " + DateTime.Now.ToString("d") + " ---- Entered Maintanance Mode---- " + DateTime.Now.ToString("T") + " #");
                Console.WriteLine("# ----------------------------------- #");
                Console.WriteLine("# Maintanance Duration    # " + Utils.Padding(durations.ToString()) + " #");
                Console.WriteLine("# Maintanance End Time    # " + Utils.Padding(Constants.Maintenance.GetEndTime.ToString("T")) + " #");
                Console.WriteLine("# ----------------------------------- #");
                Timer Timer2 = new Timer
                {
                    Interval = TimeSpan.FromSeconds(Constants.Maintenance.GetRemainingSeconds(DateTime.Now)).TotalMilliseconds,
                    AutoReset = false
                };
                Timer2.Start();
                this.LTimers.Add(5, Timer2);

                Timer2.Elapsed += (_Sender2, _Args2) =>
                {
                    Console.WriteLine("# " + DateTime.Now.ToString("d") + " ---- Exited from Maintanance Mode---- " + DateTime.Now.ToString("T") + " #");
                    Constants.Maintenance = null;
                    Timer2.Stop();
                    this.LTimers.Remove(4);
                    this.LTimers.Remove(5);
                };
            };

            this.LTimers.Add(4, Timer);
        }

        internal void Random()
        {
            Timer Timer = new Timer
            {
                Interval = TimeSpan.FromHours(1).TotalMilliseconds,
                AutoReset = true
            };
            Timer.Elapsed += (_Sender, _Args) =>
            {
                Resources.Random = new Random(DateTime.Now.ToString().GetHashCode());
            };
            this.LTimers.Add(3, Timer);
        }

        internal void Save()
        {
            Timer Timer = new Timer
            {
                Interval = 60000,
                AutoReset = true
            };

            Timer.Elapsed += (_Sender, _Args) =>
            {
#if DEBUG
                Loggers.Log(
                    Utils.Padding(this.GetType().Name, 6) + " : Save executed at " + DateTime.Now.ToString("T") + ".",
                    true);
#endif
                try
                {
                    lock (Resources.Players.Gate)
                    {
                        if (Resources.Players.Count > 0)
                        {
                            List<Level> Players = Resources.Players.Values.ToList();

                            Parallel.ForEach(Players, (_Player) =>
                            {
                                if (_Player != null)
                                {
                                    _Player.Tick();
                                    Resources.Players.Save(_Player, Constants.Database);
                                }
                            });
                        }
                    }
                    lock (Resources.Clans.Gate)
                    {
                        if (Resources.Clans.Count > 0)
                        {
                            List<Clan> Clans = Resources.Clans.Values.ToList();

                            foreach (Clan _Clan in Clans)
                            {
                                if (_Clan != null)
                                {
                                    Resources.Clans.Save(_Clan, Constants.Database);
                                }
                            }
                        }
                    }
                    lock (Resources.Battles.Gate)
                    {
                        if (Resources.Battles.Count > 0)
                        {
                            List<Battle> Battles = Resources.Battles.Values.ToList();

                            foreach (Battle _Battle in Battles)
                            {
                                if (_Battle != null)
                                {
                                    Resources.Battles.Save(_Battle, Constants.Database);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Resources.Exceptions.Catch(ex, "[: Failed at " + DateTime.Now.ToString("T") + ']' + Environment.NewLine + ex.StackTrace);
                    Loggers.Log(
                        Utils.Padding(ex.GetType().Name, 15) + " : " + ex.Message + ".[: Failed at " +
                        DateTime.Now.ToString("T") + ']' + Environment.NewLine + ex.StackTrace, true, Defcon.ERROR);
                    return;
                }
#if DEBUG

                Loggers.Log(
                    Utils.Padding(this.GetType().Name, 6) + " : Save finished at " + DateTime.Now.ToString("T") + ".",
                    true);
#endif
            };

            this.LTimers.Add(1 ,Timer);
        }
        internal void DeadSockets()
        {
            Timer Timer = new Timer
            {
                Interval = 30000,
                AutoReset = true
            };

            Timer.Elapsed += (_Sender, _Args) =>
            {
                List<Device> DeadSockets = new List<Device>();
#if DEBUG
                Loggers.Log(Utils.Padding(this.GetType().Name, 6) + " : DeadSocket executed at " + DateTime.Now.ToString("T") + ".", true);
#endif
                foreach (Device Device in Resources.Devices.Values.ToList())
                {
                    if (!Device.Connected())
                    {
                        DeadSockets.Add(Device);
                    }
                }

#if DEBUG
                Loggers.Log(
                    Utils.Padding(this.GetType().Name, 6) + " : Added " + DeadSockets.Count +
                    " devices to DeadSockets list.", true);

#endif
                foreach (Device Device in DeadSockets)
                {
                    Resources.Gateway.Disconnect(Device.Token.Args);
                }


#if DEBUG
                Loggers.Log(
                    Utils.Padding(this.GetType().Name, 6) + " : DeadSocket finished at " + DateTime.Now.ToString("T") +
                    ".", true);

#endif
            };

            this.LTimers.Add(2, Timer);
        }

        internal void Run()
        {
            foreach (Timer Timer in this.LTimers.Values)
            {
                Timer.Start();
            }
        }
    }
}