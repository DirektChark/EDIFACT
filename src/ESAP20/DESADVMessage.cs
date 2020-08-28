using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EDIFACT.ESAP20
{
    public class DESADVMessage : EDIMessage
    {
        public override IEnumerable<Segment> CreateMessage()
        {
            return base.CreateMessage(this.FullMessageEnumerator());
        }

        IEnumerable<Segment> GetControlSums(SegmentCollection segments)
        {
            decimal ControlQuantity = segments.Where(s => s.Tag == "QTY").Sum(qty => Helpers.SegmentHelpers.GetQtyValue(qty));
            int LineCount = segments.Where(s => s.Tag == "LIN").Count();
            int SegmentCount = segments.Count();

            for (int i = SegmentCount - 1; i > 0; --i)
            {
                if (segments[i].Tag == "BGM")
                {
                    SegmentCount -= i;
                    break;
                }
            }

            return Helpers.Interchange.GetMessageTrailer(ControlQuantity, LineCount);
        }

        IEnumerable<Segment> FullMessageEnumerator()
        {
            foreach (Segment seg in this)
            {
                yield return seg;
            }
            foreach(Segment seg in GetControlSums(this))
            {
                yield return seg;
            }
        }

    }
}
