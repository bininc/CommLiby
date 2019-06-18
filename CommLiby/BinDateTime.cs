using System;

namespace CommLiby
{
    public class BinDateTime : IComparable, IComparable<BinDateTime>
    {
        public static string DefaultFormatDateStr = DateTimeHelper.FormatDateStr;

        public DateTime DateTime { get; set; }

        public static BinDateTime Now()
        {
            return new BinDateTime(DateTimeHelper.Now);
        }

        public static BinDateTime New(DateTime dateTime)
        {
            return new BinDateTime(dateTime);
        }

        public BinDateTime()
        {
        }

        public BinDateTime(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public override string ToString()
        {
            return DateTime.ToString(DefaultFormatDateStr);
        }

        public static BinDateTime NewWithDateTime(DateTime dateTime)
        {
            return new BinDateTime(dateTime);
        }

        public int CompareTo(BinDateTime other)
        {
            long internalTicks = other.DateTime.Ticks;
            long num2 = DateTime.Ticks;
            if (num2 > internalTicks)
            {
                return 1;
            }
            if (num2 < internalTicks)
            {
                return -1;
            }
            return 0;
        }

        public int CompareTo(object value)
        {
            if (value == null)
            {
                return 1;
            }
            if (!(value is BinDateTime))
            {
                throw new ArgumentException("必须是BinDateTime类型");
            }
            BinDateTime time = (BinDateTime)value;
            long internalTicks = time.DateTime.Ticks;
            long num2 = DateTime.Ticks;
            if (num2 > internalTicks)
            {
                return 1;
            }
            if (num2 < internalTicks)
            {
                return -1;
            }
            return 0;
        }
    }
}
