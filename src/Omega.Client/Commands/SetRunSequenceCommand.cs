using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Client.Commands
{
    public enum RunSequenceMode : byte
    {
        /// <summary>
        /// All TEXT files in the run sequence will run according to their associated times.
        /// </summary>
        RespectTime,

        /// <summary>
        /// All TEXT files in the run sequence will run in order, regardless of their associated times.
        /// </summary>
        IgnoreTime,

        /// <summary>
        /// All TEXT files in the run sequence will run 
        /// </summary>
        Delete
    }

    public class SetRunSequenceCommand : WriteSpecialCommand
    {
        public SetRunSequenceCommand()
            : base("\x2E") {}

        protected override IEnumerable<byte> GetSpecialFunctionData()
        {
            throw new NotImplementedException();
        }
    }
}
