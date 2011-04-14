using System;
using System.Collections;
using System.Collections.Generic;
using Omega.Client.Commands;
using Omega.Client.FileSystem;

namespace Omega.Client
{
    public class Packet : IEnumerable<Command>
    {
        private readonly Queue<Command> commands
            = new Queue<Command>();

        public SignType SignType { get; set; }
        public SignAddress Address { get; set; }

        public Packet(SignType type = SignType.AllSigns, SignAddress address = null)
        {
            SignType = type;
            Address = address ?? SignAddress.Broadcast;
        }

        public void Add(Command command)
        {
            commands.Enqueue(command);
        }

        #region Implementation of IEnumerable

        public IEnumerator<Command> GetEnumerator()
        {
            return commands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region TEXT

        public void WriteText(IEnumerable<TextFile> files)
        {
            foreach(var file in files)
                Add(new WriteTextCommand(file));
        }

        public void WritePriorityMessage(string text, DisplayMode mode = DisplayMode.AutoMode)
        {
            var file = new TextFile(FileLabel.Priority) {
                new TextFileLine(text, mode)
            };

            Add(new WriteTextCommand(file));
        }

        public void ClearPriorityMessage()
        {
            WritePriorityMessage("");
        }

        #endregion

        #region SPECIAL

        public void SetMemory(FileTable config)
        {
            Add(new SetMemoryCommand(config));
        }

        public void SetMemory(IEnumerable<SignFile> files)
        {
            SetMemory(new FileTable(files));
        }

        public void ClearMemory()
        {
            Add(new SetMemoryCommand());
        }

        public void Reset()
        {
            Add(new ResetCommand());
        }

        public void SetDateTime(DateTime datetime)
        {
            Add(new SetDateCommand(datetime));
            Add(new SetDayCommand(datetime.DayOfWeek));
            Add(new SetTimeCommand(datetime));
        }

        #endregion
    }
}