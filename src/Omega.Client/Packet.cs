using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Omega.Client.Commands;
using Omega.Client.Connection;
using Omega.Client.FileSystem;

namespace Omega.Client
{
    public class Packet : IEnumerable<Command>
    {
        private readonly Stream stream;
        private readonly PacketFormat format;
        private readonly SignType signType;
        private readonly SignAddress address;

        private readonly Queue<Command> commands
            = new Queue<Command>();
        
        public Packet(Stream stream, PacketFormat format, SignType signType, SignAddress address)
        {
            this.stream = stream;
            this.format = format;
            this.signType = signType;
            this.address = address;
        }

        #region Basic Commands

        public virtual void Add(Command command)
        {
            commands.Enqueue(command);
        }

        public virtual void Send()
        {
            if(commands.Count <= 0)
                return;

            var writer = format.CreateWriter(stream);
            writer.WriteHeader(signType, address);
            foreach(var command in commands)
                writer.WriteCommand(command);
            writer.EndTransmission();
        }

        #endregion

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

        #region TEXT Commands

        public void WritePriorityMessage(string text, DisplayMode mode = DisplayMode.AutoMode)
        {
            var textFile = new TextFile(FileLabel.Priority);
            textFile.Add(new TextFileLine(text, mode));
            Add(new WriteTextCommand(textFile));
        }

        public void WriteDefaultMessage(string text, DisplayMode mode = DisplayMode.AutoMode)
        {
            var textFile = new TextFile(FileLabel.Default);
            textFile.Add(new TextFileLine(text, mode));
            Add(new WriteTextCommand(textFile));
        }

        #endregion

        #region Memory Methods

        public void ClearMemory()
        {
            Add(new SetMemoryCommand());
        }

        public void SetMemory(MemoryConfig config)
        {
            Add(new SetMemoryCommand(config));
        }

        #endregion

        #region Other SPECIAL Methods

        public void Reset()
        {
            Add(new ResetCommand());
        }

        public void SetDateTime(DateTime datetime)
        {
            // TODO: SetDate will cause a TRANSMISSION ERROR message on 221b, which is ignored, but it'd be nice
            // to handle sign capabilities.
            Add(new SetDateCommand(datetime));
            Add(new SetDayCommand(datetime.DayOfWeek));
            Add(new SetTimeCommand(datetime));
        }

        #endregion
    }
}
