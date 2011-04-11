using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Client.Commands
{
    public class SetDayCommand : WriteSpecialCommand
    {
        public DayOfWeek Day { get; set; }

        public SetDayCommand(DayOfWeek day)
            : base("&")
        {
            Day = day;
        }

        protected override IEnumerable<byte> GetSpecialFunctionData()
        {
            yield return (byte) (0x31 + (byte) Day);
        }
    }
}
