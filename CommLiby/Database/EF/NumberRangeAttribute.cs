using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommLiby.Database.EF
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NumberRangeAttribute : Attribute
    {

        public NumberRangeAttribute(byte interget,byte point=0)
        {
            this.Interger = interget;
            this.Point = point;
        }

        public byte Interger { get; private set; }
        public byte Point { get; private set; }
    }
}
