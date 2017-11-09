using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Sideline.Loadtest
{
    class Program
    {
        private static bool running = false;
        static void Main(string[] args)
        {
            var Settings = ParseArgs(args);
            if (Settings == null) return;

            if (Settings.AskForUser)
            {
                Console.WriteLine();
                Console.WriteLine("Ange namnet på användaren:");
                Settings.Username = Console.ReadLine();
            }
            var su = new SidelineAPI(Settings.BaseUrl);
            Console.WriteLine(su.Login(Settings.Username, Settings.Password, Settings.Database).Result);
            Console.WriteLine(su.Stats().Result);
            Console.WriteLine(su.JoinMatch().Result);
            Console.WriteLine("Tryck en tangent för att starta");
            Console.ReadKey();

            running = true;
            var sw = new Stopwatch();
            var sw2 = new Stopwatch();
            sw.Start();

            //Background job calls matchstate every second.
            Task.Run(()=>
            {
                var sw3 = new Stopwatch();
                while (running)
                {
                    Thread.Sleep(1000);
                    sw3.Reset();
                    sw3.Start();
                    su.MatchState().Wait();
                    sw3.Stop();
                    Console.WriteLine($"MatchState: {sw3.ElapsedMilliseconds}ms");
                }
            });

            //Run and Coin for x iterations each separated by delay.
            for (int i = 0; i < Settings.Iterations; i++)
            {
                Thread.Sleep(Settings.DelayMult);
                sw2.Reset();
                sw2.Start();
                su.Run (1, (i % 31) + 1).Wait();
                sw2.Stop();
                Console.WriteLine($"Iteration {i} - Run: {sw2.ElapsedMilliseconds}ms");
                
                Thread.Sleep(Settings.DelayMult);
                sw2.Reset();
                sw2.Start();
                su.Coin(1, (i % 31) + 1).Wait();
                sw2.Stop();
                Console.WriteLine($"Iteration {i} - Coin: {sw2.ElapsedMilliseconds}ms");
            }

            //Cleanup
            running = false;
            Console.WriteLine($"Finished after {sw.ElapsedMilliseconds}ms");
            sw.Stop();
            sw2.Stop();
        }

        private static Settings ParseArgs(string[] args)
        {
            var settings = new Settings();
            var x = new CommandLineParser.CommandLineParser();

            try
            {
                x.ExtractArgumentAttributes(settings);
                x.ParseCommandLine(args);
            }
            catch(Exception)
            {
                x.ShowUsage();
                return null;
            }
            return settings;
        }
    }
}
