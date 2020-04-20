using System;
using System.Collections.Generic;
using System.Text;

namespace EDIFACT.Segments
{
    public class DataNull
    {
        public static readonly DataNull Value;

        static DataNull()
        {
            Value = new DataNull();
        }
    }
}
