using System;
using System.Collections.Generic;
using System.Text;

namespace EDIFACT.Segments
{
    
    public abstract class Segment
    {
        DynamicSegment segment;

        public Segment(params object[] args)
        {

        }

        public override string ToString()
        {
            return segment.ToString();
        }
    }
}
