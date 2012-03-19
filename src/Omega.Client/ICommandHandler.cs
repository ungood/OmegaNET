using System;
using System.Collections.Generic;
using Omega.Client.Commands;
using Omega.Client.Memory;

namespace Omega.Client
{
    public interface ICommandHandler
    {
        void Handle(Command command);
    }

    public static class CommandHandlerExtensions
    {
        public static void WriteText(this ICommandHandler handler, IEnumerable<TextFile> files)
        {
            foreach(var file in files)
                handler.Handle(new WriteTextCommand(file));
        }

        public static void WritePriorityMessage(this ICommandHandler handler, string text, DisplayMode mode = DisplayMode.AutoMode)
        {
            var file = new TextFile(FileLabel.Priority) {
                new TextFileLine(text, mode)
            };

            handler.Handle(new WriteTextCommand(file));
        }

        public static void ClearPriorityMessage(this ICommandHandler handler)
        {
            WritePriorityMessage(handler, "");
        }

        public static void SetMemory(this ICommandHandler handler, FileTable config)
        {
            handler.Handle(new SetMemoryCommand(config));
        }

        public static void SetMemory(this ICommandHandler handler, IEnumerable<SignFile> files)
        {
            SetMemory(handler, new FileTable(files));
        }

        public static void ClearMemory(this ICommandHandler handler)
        {
            handler.Handle(new SetMemoryCommand());
        }

        public static void Reset(this ICommandHandler handler)
        {
            handler.Handle(new ResetCommand());
        }

        public static void SetDateTime(this ICommandHandler handler, DateTime datetime)
        {
            handler.Handle(new SetDateCommand(datetime));
            handler.Handle(new SetDayCommand(datetime.DayOfWeek));
            handler.Handle(new SetTimeCommand(datetime));
        }
    }
}
