using System;
using System.Collections.Generic;
using System.Text;

namespace EDIFACT.Segments
{

    public class LIN : Segment
    {
        DynamicSegment segment;
        public LIN(params object[] args)
        {
            segment = new DynamicSegment("LIN")
                .AddElement(args[0])
                .AddElement(args[1])
                .AddComposite(args[2], args[3], args[4], args[5]);
        }


    }
}
