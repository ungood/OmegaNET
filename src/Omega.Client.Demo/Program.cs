using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using Omega.Client.Commands;
using Omega.Client.FileSystem;
using Omega.Client.Formatting;
using Omega.Client.Serial;

namespace Omega.Client.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleListener = new TextWriterTraceListener(Console.Out);
            Trace.Listeners.Add(consoleListener);

            var portName = "COM1";
            //var portName = args.Length > 1 ? args[1] : GetPortName();
            using(var conn = new SerialConnection(portName))
            {
                conn.Open();

                //Clear(conn);
                Thread.Sleep(10000);

                //Blah(conn);
                DemoWriteTextCommands(conn);
            }

            //Console.ReadLine();
        }

        private static void Clear(IConnection conn)
        {
            var packet = conn.CreatePacket();
            packet.ClearMemory();
            packet.Send();
        }

        private static void Blah(IConnection conn)
        {
            var file = new TextFile('A') {
                {"IF YOU WANT YOUR STAPLER BRING ONE ... MILLION DOLLARS TO THE TORCH AT MIDNIGHT", DisplayMode.Rotate},
                {"ANONYMOUS", SpecialMode.Sparkle},
            };

            var packet = conn.CreatePacket();
            packet.SetMemory(new MemoryConfig {file});
            packet.Add(new WriteTextCommand(file));
            packet.Send();
        }

        private static void DemoWriteTextCommands(IConnection conn)
        {
            var packet = conn.CreatePacket();

            var files = new List<TextFile>() {
                //DemoDisplayModes(),
                //DemoSpecialModes(),
                //DemoTime(packet),
                DemoFormatting()
            };
            files.AddRange(DemoGraphics());

            var memory = new MemoryConfig();
            foreach(var file in files)
                memory.Add(file);
            packet.SetMemory(memory);

            foreach(var file in files)
                packet.Add(new WriteTextCommand(file));
            
            packet.Send();
        }

        private static TextFile DemoDisplayModes()
        {
            return new TextFile('B') {
                {"ROTATE ROTATE ROTATE ROTATE ROTATE ROTATE ROTATE ROTATE ROTATE ROTATE ROTATE", DisplayMode.Rotate},
                {"HOLD", DisplayMode.Hold},
                {"FLASH", DisplayMode.Flash},
                {"ROLL UP", DisplayMode.RollUp},
                {"ROLL DOWN", DisplayMode.RollDown},
                {"ROLL LEFT", DisplayMode.RollLeft},
                {"ROLL RIGHT", DisplayMode.RollRight},
                {"ROLL IN", DisplayMode.RollIn},
                {"ROLL OUT", DisplayMode.RollOut},
                {"WIPE UP", DisplayMode.WipeUp},
                {"WIPE DOWN", DisplayMode.WipeDown},
                {"WIPE LEFT", DisplayMode.WipeLeft},
                {"WIPE RIGHT", DisplayMode.WipeRight},
                {"WIPE IN", DisplayMode.WipeIn},
                {"WIPE OUT", DisplayMode.WipeOut},
                {"SCROLL", DisplayMode.Scroll},
                {"AUTOMODE", DisplayMode.AutoMode},
                {"ROTATE COMPRESSED ROTATE COMPRESSED ROTATE COMPRESSED ROTATE COMPRESSED ROTATE COMPRESSED ROTATE", DisplayMode.CompressedRotate},
                {"EXPLODE", DisplayMode.Explode},
                {"CLOCK", DisplayMode.Clock}
            };
        }

        private static TextFile DemoSpecialModes()
        {
            return new TextFile('C') {
                {"TWINKLE", SpecialMode.Twinkle},
                {"SPARKLE", SpecialMode.Sparkle},
                {"SNOW", SpecialMode.Snow},
                {"INTERLOCK", SpecialMode.Interlock},
                {"SWITCH", SpecialMode.Switch},
                {"SLIDE", SpecialMode.Slide},
                {"SPRAY", SpecialMode.Spray},
                {"STARBURST", SpecialMode.Starburst},
                {"", SpecialMode.Welcome},
                {"", SpecialMode.SlotMachine},
                {"NEWS FLASH", SpecialMode.NewsFlash},
                {"TRUMPET", SpecialMode.Trumpet},
                {"CYCLE COLORS", SpecialMode.CycleColors},
            };
        }

        private static IEnumerable<TextFile> DemoGraphics()
        {
            return new List<TextFile> {
                new TextFile('D') {SpecialGraphic.ThankYou},
                new TextFile('E') {SpecialGraphic.NoSmoking},
                new TextFile('F') {SpecialGraphic.DontDrinkAndDrive},
                new TextFile('G') {SpecialGraphic.RunningAnimal},
                new TextFile('H') {SpecialGraphic.Fireworks},
                new TextFile('I') {SpecialGraphic.TurboCar},
                new TextFile('J') {SpecialGraphic.CherryBomb},
            };
        }

        private static TextFile DemoTime(Packet packet)
        {
            packet.SetDateTime(DateTime.Now);

            return new TextFile('K') {
                {"\u0013", DisplayMode.AutoMode}
            };
        }

        private static TextFile DemoFormatting()
        {
            return new TextFile('L') {
                {"ÇüéâäàåçêëèïîìÄÅÉæÆôöòûùÿÖÜ¢£¥℞ƒáíóúñÑªº¿°¡ øØćĆčČđÐŠžŽΒšβÁÀÃãÊÍÕõ€ ↑↓←→", DisplayMode.Rotate}
            };
        }

        private static void DemoStrings(IConnection conn)
        {
            var packet = conn.CreatePacket();

            var text = new TextFile('A') {
                {"\u0019 \u0010C", DisplayMode.Scroll}
            };

            packet.SetMemory(new MemoryConfig {
                {'C', new StringFileInfo(10)},
                text
            });

            packet.Add(new WriteTextCommand(text));
            packet.Send();

            for(int i = 0; i < 60; i++)
            {
                packet = conn.CreatePacket();
                packet.Add(new WriteStringCommand(new StringFile('C', i.ToString("000"))));
                packet.Send();
                Thread.Sleep(1000);
            }
        }

        private static string GetPortName()
        {
            var ports = SerialPort.GetPortNames();
            Console.WriteLine("Available ports: " + string.Join(", ", ports));
            return Console.ReadLine();
        }
    }
}
