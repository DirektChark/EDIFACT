using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EDIFACT.Helpers
{
    class SegmentHelpers
    {
        public static string TrimSegment(string segmentString)
        {
            segmentString = Regex.Replace(segmentString, "\\++'$", "'");

            return segmentString;
        }
    }
}
