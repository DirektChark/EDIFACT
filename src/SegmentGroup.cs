using EDIFACT.Segments;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EDIFACT
{
    public class SegmentGroup : IEnumerable<DynamicSegment>
    {
        internal List<DynamicSegment> segments = new List<DynamicSegment>();

        public DynamicSegment this[int i] => segments[i];
            

        List<DynamicSegment> l;
        public SegmentGroup()
        {
        }

        public SegmentGroup(IEnumerable<DynamicSegment> segments)
        {            
            this.segments.AddRange(segments);
        }


        public Segments.DynamicSegment AddSegment(string tag)
        {
            var s = new Segments.DynamicSegment(tag);
            segments.Add(s);
            return s;
        }

        public void Add(DynamicSegment segment)
        {
            this.segments.Add(segment);
        }

        /*
        public IEnumerator GetEnumerator()
        {
            return segments.GetEnumerator();
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            IEnumerable<DynamicSegment> enumerable = segments;
            return enumerable.Select(x => x.ToString()).GetEnumerator();
        }*/

        IEnumerator<DynamicSegment> IEnumerable<DynamicSegment>.GetEnumerator()
        {
            return ((IEnumerable<DynamicSegment>)this.segments).GetEnumerator();
        }

        public IEnumerator<string> GetStringEnumerator()
        {
            return segments.Select(x => x.ToString()).GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<DynamicSegment>)this.segments).GetEnumerator();
        }
    }
}
