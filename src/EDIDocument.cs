using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EDIFACT.Segments;

namespace EDIFACT
{
    public class EDIDocument : SegmentGroup
    {
        public void AddSegmentGroup(SegmentGroup segmentGroup)
        {
            AddSegments(segmentGroup);
        }

        public void AddSegments(IEnumerable<DynamicSegment> enumerable)
        {
            this.segments.AddRange(enumerable.Select(x => x));
        }


        public SegmentGroup AppendControlSegment()
        {
            var v = DesadvHelper.GetInterchangeFooter(interchangeDto);
            decimal ControlQuantity = 489m,
                LineCount = 55,
                SegmentCount = 35,
                InterchangeControlCount = 1;

            var sg = new SegmentGroup();

            sg.AddSegment("CNT");
            sg.AddSegment("CNT");
            sg.AddSegment("CNT");
            sg.AddSegment("CNT");
            segments.AddRange(sg);
            return sg;
        }
    }
}
