using System;
using System.Collections.Generic;
using System.Text;

namespace EDIFACT
{
    public struct DataElementPadding
    {
        int n;
        public DataElementPadding(int n)
        {
            this.n = n;
        }

        public override string ToString()
        {
            return new string('+', n);
        }
    }
}
