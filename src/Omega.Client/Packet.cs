using System;
using System.Collections;
using System.Collections.Generic;
using Omega.Client.Commands;
using Omega.Client.FileSystem;

namespace Omega.Client
{
    public partial class Packet : IEnumerable<Command>
    {
        private readonly Queue<Command> commands
            = new Queue<Command>();

        public SignType SignType { get; set; }
        public SignAddress Address { get; set; }

        public Packet(SignType type = SignType.AllSignsVerify, SignAddress address = null)
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

        #region SetMemory

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

        #endregion
        
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
    }
}