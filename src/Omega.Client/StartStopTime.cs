using System;

namespace Omega.Client
{
    public struct StartStopTime
    {
        public byte Value { get; private set; }
        public TimeSpan Time { get; private set; }

        public StartStopTime(TimeSpan time) : this()
        {
            Time = time;
            var minutes = (int) time.TotalMinutes;
            Value = (byte) (minutes / 10);
        }

        public StartStopTime(byte value) : this()
        {
            if(value <= 0x8F)
            {
                var minutes = value * 10;
                Time = new TimeSpan(minutes / 60, minutes % 60, 0);
            }

            Value = value;
        }

        #region Casts

        public static implicit operator byte(StartStopTime time)
        {
            return time.Value;
        }

        public static implicit operator StartStopTime(byte value)
        {
            return new StartStopTime(value);
        }

        public static implicit operator TimeSpan(StartStopTime time)
        {
            if(time.Value >= 0x8F)
                return TimeSpan.MinValue;

            return time.Time;
        }

        public static implicit operator StartStopTime(TimeSpan timespan)
        {
            return new StartStopTime(timespan);
        }

        #endregion

        public override string ToString()
        {
            return Value.ToString("X2");
        }

        public static readonly StartStopTime AllDay = new StartStopTime(0xFD);
        public static readonly StartStopTime Never = new StartStopTime(0xFE);
        public static readonly StartStopTime Always = new StartStopTime(0xFF);
    }
}