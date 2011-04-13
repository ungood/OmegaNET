using System;
using System.Text.RegularExpressions;

namespace Omega.Client
{
    public class SignAddress
    {
        public string Value { get; private set; }

        private static readonly Regex AddressRegex = new Regex("[0-9A-F?][0-9A-F?]");

        public SignAddress(string address)
        {
            if(!AddressRegex.IsMatch(address))
                throw new ArgumentOutOfRangeException("address", address + " is not a valid value for a SignAddress");

            Value = address;
        }
        
        public static implicit operator SignAddress(ushort address)
        {
            return new SignAddress(address.ToString("X2"));
        }

        public override string ToString()
        {
            return Value;
        }

        public static readonly SignAddress Broadcast = 0x00;
    }
}
