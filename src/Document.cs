using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EDIFACT
{
    public class Document : IEnumerable
    {
        List<Segments.Segment> segments = new List<Segments.Segment>();
        public void AddSegment(Segments.Segment segment) { segments.Add(segment); }

        public IEnumerator GetEnumerator()
        {
            return segments.GetEnumerator();
        }
    }
}
