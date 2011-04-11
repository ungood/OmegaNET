using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Client.Connection
{
    public class Checksum
    {
        private ushort value;

        public void Add(byte b)
        {
            unchecked
            {
                value += b;
            }
        }

        public override string ToString()
        {
            return value.ToString("X4");
        }
    }
}
