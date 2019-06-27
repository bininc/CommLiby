using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommLiby.Compress
{
    public class StreamException : IOException
    {
        public StreamException()
        {
        }

        public StreamException(string msg)
            : base(msg)
        {
        }
    }
}
