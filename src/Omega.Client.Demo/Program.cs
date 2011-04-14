﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Windows.Media.Imaging;
using Omega.Client.Commands;
using Omega.Client.Connection;
using Omega.Client.FileSystem;

namespace Omega.Client.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var consoleListener = new TextWriterTraceListener(Console.Out);
            Trace.Listeners.Add(consoleListener);

            var portName = args.Length > 1 ? args[1] : GetPortName();

            using(var conn = new SerialConnection(portName))
            {
                conn.Open();

                //conn.Send(Clear());
                //Thread.Sleep(10000);

                conn.Send(DemoPicture());
                //DemoWriteTextCommands(conn);
            }

            Console.ReadLine();
        }

        private static Packet Clear()
        {
            var packet = new Packet();
            packet.ClearMemory();
            return packet;
        }

        private static Packet BlackfinDemo()
        {
            var files = new List<TextFile> {
                new TextFile('A') {
                    {"<speed 5/>IF YOU WANT YOUR STAPLER BRING <speed 1/>ONE. MILLION. DOLLARS.<speed 5/> TO THE TORCH AT MIDNIGHT", DisplayMode.Rotate},
                },
                new TextFile('B') {
                    SpecialGraphic.DontDrinkAndDrive
                },
                new TextFile('E') {
                    {"THE WEATHER IS WARM WITH A CHANCE OF RAIN..."},
                },
                new TextFile('C') {
                    {"WELCOME TO BLACKFIN<line/>NOW 100% MORE", DisplayMode.Scroll},
                    {"<wide>XTREME</wide>", SpecialMode.Sparkle}
                },
                new TextFile('D') {"<speed 1/><time/>"}
            };
            
            var packet = new Packet();
            packet.SetMemory(files);
            packet.WriteText(files);
            return packet;
        }

        private static Packet DemoWriteTextCommands()
        {
            var packet = new Packet();

            var files = new List<TextFile> {
                DemoDisplayModes(),
                DemoSpecialModes(),
                DemoFormatting()
            };
            files.AddRange(DemoGraphics());

            packet.SetMemory(files);
            packet.WriteText(files);

            return packet;
        }

        #region Text File Modes

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
                {"ROTATE COMPRESSED ROTATE COMPRESSED ROTATE COMPRESSED ROTATE COMPRESSED ROTATE COMPRESSED ROTATE",
                    DisplayMode.CompressedRotate},
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

        #endregion

        #region Formatting

        private static TextFile DemoFormatting()
        {
            return new TextFile('K') {
                {"<flash>FLASH</flash>", DisplayMode.Scroll},
                {"<high>FLASH</high>", DisplayMode.Scroll},
                {"<desc>DESCENDERS</desc>", DisplayMode.Scroll},
                {"<wide>WIDE</wide>", DisplayMode.Scroll},
                {"<double>DOUBLE WIDE</double>", DisplayMode.Scroll},
                {"<fixed>FIXED</fixed>", DisplayMode.Scroll},
                {"<fancy>FANCY</fancy>", DisplayMode.Scroll},
                {"<shadows>FLASH</shadows>", DisplayMode.Scroll},
            };
        }

        private static TextFile DemoTime(Packet packet)
        {
            packet.SetDateTime(DateTime.Now);

            return new TextFile('L') {
                {"\u0013", DisplayMode.AutoMode}
            };
        }

        private static TextFile DemoExtendedChars()
        {
            return new TextFile('M') {
                {"\x15ÇüéâäàåçêëèïîìÄÅÉæÆôöòûùÿÖÜ¢£¥℞ƒáíóúñÑªº¿°¡ øØćĆčČđÐŠžŽΒšβÁÀÃãÊÍÕõ€\x08\x63↑↓←→", DisplayMode.Rotate}
            };
        }

        #endregion

        #region Pictures

        private static Packet DemoPicture()
        {
            var uri = new Uri("120x7-gradient.png", UriKind.Relative);
            var image = new BitmapImage(uri);

            var pic = new PictureFile('P', image, ColorFormat.Monochrome);
            var text = new TextFile('A') {{"WARNING                    \x14P                           ", DisplayMode.Rotate}};

            var files = new FileTable {
                text, pic,
            };

            var packet = new Packet();
            packet.SetMemory(files);
            packet.Add(new WriteTextCommand(text));
            packet.Add(new WritePictureCommand(pic));
            return packet;
        }

        #endregion

        private static void DemoStrings(IConnection conn)
        {
            var packet = new Packet();

            var text = new TextFile('A') {
                {"<string C/>", DisplayMode.Scroll}
            };

            packet.SetMemory(new FileTable {
                {'C', new StringFileInfo(10)},
                text
            });

            packet.Add(new WriteTextCommand(text));
            conn.Send(packet);

            for(int i = 0; i < 60; i++)
            {
                packet = new Packet();
                packet.Add(new WriteStringCommand(new StringFile('C', i.ToString("000"))));
                conn.Send(packet);
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