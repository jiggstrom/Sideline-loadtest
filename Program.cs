using Newtonsoft.Json;
using Sideline.Loadtest.viewmodels;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

namespace Sideline.Loadtest
{
    class Program
    {
        private static bool running = false;
        static void Main(string[] args)
        {
            string res = "";
            var settings = ParseArgs(args);
            if (settings == null) return;

            if (settings.AskForUser)
            {
                Console.WriteLine();
                Console.WriteLine("Ange namnet på användaren:");
                settings.Username = Console.ReadLine();
            }

            var su = new SidelineAPI(settings.BaseUrl);
            LoginResultObject login;
            if(!string.IsNullOrEmpty(settings.Token))
                login = su.FacebookLogin(settings.Token, settings.Database).Result;
            else
                login = su.Login(settings.Username, settings.Password, settings.Database).Result;
            Console.WriteLine($"Playerid = {login.PlayerId}");
            Console.WriteLine($"SessionId = {login.SessionId}");
            Console.WriteLine($"StatusInfo = {login.StatusInfo}");
            Console.WriteLine($"VersionExpired = {login.VersionExpired}");

            Console.WriteLine($"LigaId = {su.Stats().Result?.LigaInfo?.LigaId}");

            var match = su.JoinMatch().Result;
            var startTime = (DateTimeOffset.FromUnixTimeSeconds(match.datum).DateTime).ToLocalTime();
            var duration = match.duration;
            Console.WriteLine($"Match start at {startTime:G} with a duration of {duration/60} minutes.");

            Console.WriteLine($"Now = {DateTime.Now}");
            Console.WriteLine($"startTime = {startTime}");
            Console.WriteLine($"Seconsdsdiff = {(DateTime.Now - startTime).TotalSeconds}");
            Console.WriteLine($"duration = {duration}");
            Console.WriteLine($"dur/slot = {(duration / 32)}");
            Console.WriteLine($"curerntslot = {(int)((DateTime.Now - startTime).TotalSeconds) / (duration / 32)}");

            if (settings.Delay && (DateTime.Now - startTime).TotalSeconds > 0)
            {

                Console.WriteLine($"Waiting for match start in {(DateTime.Now - startTime).TotalSeconds}  sec");
                Console.CursorLeft = 0;
                Thread.Sleep(1000);
            }

            var wait = new Random().Next(0, 5000);
            Console.WriteLine($"Waiting for {wait} milliseconds before starting.");
            Thread.Sleep(wait);
            Console.Clear();
            Console.WindowHeight = 20;
            Console.WindowWidth = 70;
            running = true;
            var sw = new Stopwatch();
            var sw2 = new Stopwatch();
            sw.Start();

            //Background job calls matchstate every second.
            Task.Run(() =>
            {
                GetMatchStatePeriodically(su);
            });

            Task.Run(() =>
            {
                GetPlayerinfoPeriodically(su);
            });

            
            var delayBetweenCoin = ((float)duration / (float)settings.Iterations);
            if (delayBetweenCoin < 0.1f) delayBetweenCoin = 5f;
            Console.CursorLeft = 0;
            Console.CursorTop = 3;
            Console.WriteLine($"Deleay between coin: {delayBetweenCoin} sec");

            if (settings.IdleWatcher == false)
            {

                //Run and Coin for x iterations each separated by delay.
                var elapsedMilisecods = 0;
                for (int i = 0; i < settings.Iterations; i++)
                {
                    if(((int)(delayBetweenCoin * 1000) - elapsedMilisecods) > 0)
                        Thread.Sleep((int)(delayBetweenCoin*1000) - elapsedMilisecods);
                    sw2.Reset();

                    var currentSlot = (int)(((DateTime.Now - startTime).TotalSeconds) / (int)(duration / 32));

                    if (currentSlot < 0) currentSlot = (new Random().Next(0, 31));
                    if (currentSlot > 31)
                    {
                        Console.CursorLeft = 0;
                        Console.CursorTop = 1;
                        Console.WriteLine($"Match ended. {currentSlot}");
                        continue;
                    }

                    if (i % 10 == 0)
                    {
                        sw2.Start();
                        res = su.Run(1, currentSlot).Result;
                        sw2.Stop();
                        Console.CursorLeft = 0;
                        Console.CursorTop = 1;
                        Console.WriteLine($"Iteration {i} - Run in slot {currentSlot}: {sw2.ElapsedMilliseconds}ms");
                    }
                    else
                    {
                        sw2.Start();
                        res = su.Coin(1, currentSlot).Result;
                        sw2.Stop();
                        Console.CursorLeft = 0;
                        Console.CursorTop = 2;
                        Console.WriteLine($"Iteration {i} - Coin in slot {currentSlot}: {sw2.ElapsedMilliseconds}ms");

                    }

                    elapsedMilisecods = (int)sw2.ElapsedMilliseconds;
                }
            }
            else
            {
                for (int i = 0; i < settings.Iterations; i++)
                {

                    Thread.Sleep((int)(delayBetweenCoin * 1000));
                    Console.CursorLeft = 0;
                    Console.CursorTop = 2;
                    Console.WriteLine($"Idling {i}");

                }
            }

            //Cleanup
            running = false;
            Console.WriteLine($"Finished after {sw.ElapsedMilliseconds}ms");
            sw.Stop();
            sw2.Stop();
            Console.ReadLine();
        }

        private static void GetPlayerinfoPeriodically(SidelineAPI su)
        {
            var ack = 0L;
            var rounds = 0;
            var sw3 = new Stopwatch();
            while (running)
            {
                if (sw3.ElapsedMilliseconds < 2000)
                    Thread.Sleep(1000 - (int)sw3.ElapsedMilliseconds);
                sw3.Reset();
                sw3.Start();
                var nisse = su.Player().Result;
                sw3.Stop();
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                ack += sw3.ElapsedMilliseconds;
                rounds++;
                Console.WriteLine($"Player: {sw3.ElapsedMilliseconds}ms (avg. {ack / rounds})");
                try
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = 5;
                    Console.WriteLine($"Player: {nisse.PrivateInfo.Currency} bucks, {nisse.PrivateInfo.Energy} energy, {nisse.PrivateInfo.Gold} gold");
                }
                catch (Exception ex)
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = 5;
                    Console.WriteLine("Error getting playerinfo".PadRight(80,' '));
                }
            }
        }

        private static void GetMatchStatePeriodically(SidelineAPI su)
        {
            var ack = 0L;
            var rounds = 0;
            var sw3 = new Stopwatch();
            while (running)
            {
                if (sw3.ElapsedMilliseconds < 1000)
                    Thread.Sleep(1000 - (int)sw3.ElapsedMilliseconds);
                sw3.Reset();
                sw3.Start();
                var nisse = su.MatchState().Result;
                sw3.Stop();
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                ack += sw3.ElapsedMilliseconds;
                rounds++;
                Console.WriteLine($"MatchState: {sw3.ElapsedMilliseconds}ms (avg. {ack / rounds})");
                try
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = 6;
                    Console.WriteLine($"Matchtid: {nisse.MatchTimeInSeconds}");
                    Console.WriteLine(
                        $"Slot 1: {nisse.Slot0.Balance,-5:f2}\t Slot 9:  {nisse.Slot8.Balance,-5:f2}\tSlot 17: {nisse.Slot16.Balance,-5:f2}\tSlot 25: {nisse.Slot24.Balance,-5:f2}");
                    Console.WriteLine(
                        $"Slot 2: {nisse.Slot1.Balance,-5:f2}\tSlot 10:  {nisse.Slot9.Balance,-5:f2}\tSlot 18: {nisse.Slot17.Balance,-5:f2}\tSlot 26: {nisse.Slot25.Balance,-5:f2}");
                    Console.WriteLine(
                        $"Slot 3: {nisse.Slot2.Balance,-5:f2}\tSlot 11:  {nisse.Slot10.Balance,-5:f2}\tSlot 19: {nisse.Slot18.Balance,-5:f2}\tSlot 27: {nisse.Slot26.Balance,-5:f2}");
                    Console.WriteLine(
                        $"Slot 4: {nisse.Slot3.Balance,-5:f2}\tSlot 12:  {nisse.Slot11.Balance,-5:f2}\tSlot 20: {nisse.Slot19.Balance,-5:f2}\tSlot 28: {nisse.Slot27.Balance,-5:f2}");
                    Console.WriteLine(
                        $"Slot 5: {nisse.Slot4.Balance,-5:f2}\tSlot 13:  {nisse.Slot12.Balance,-5:f2}\tSlot 21: {nisse.Slot20.Balance,-5:f2}\tSlot 29: {nisse.Slot28.Balance,-5:f2}");
                    Console.WriteLine(
                        $"Slot 6: {nisse.Slot5.Balance,-5:f2}\tSlot 14:  {nisse.Slot13.Balance,-5:f2}\tSlot 22: {nisse.Slot21.Balance,-5:f2}\tSlot 30: {nisse.Slot29.Balance,-5:f2}");
                    Console.WriteLine(
                        $"Slot 7: {nisse.Slot6.Balance,-5:f2}\tSlot 15:  {nisse.Slot14.Balance,-5:f2}\tSlot 23: {nisse.Slot22.Balance,-5:f2}\tSlot 31: {nisse.Slot30.Balance,-5:f2}");
                    Console.WriteLine(
                        $"Slot 8: {nisse.Slot7.Balance,-5:f2}\tSlot 16:  {nisse.Slot15.Balance,-5:f2}\tSlot 24: {nisse.Slot23.Balance,-5:f2}\tSlot 32: {nisse.Slot31.Balance,-5:f2}");
                    Console.CursorLeft = 0;
                    Console.CursorTop = 4;
                    Console.WriteLine(" ".PadRight(30, ' '));
                }
                catch (Exception)
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = 4;
                    Console.WriteLine("Error getting matchstate.");
                }
            }
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
