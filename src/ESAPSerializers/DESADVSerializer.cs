using System;
using System.Linq.Expressions;
using System.Text;

namespace EDIFACT.ESAPSerializers
{
    public class DESADVSerializer
    {
        private EDIInterchange interchange;
        private EDISegmentFormatter segmentFormatter;
        private StringBuilder sb;

        public string Serialize(EDIInterchange interchange)
        {
            this.interchange = interchange;
            this.segmentFormatter = new EDISegmentFormatter();

            this.sb = new StringBuilder();

            this.AddServiceStringAdvice();
            this.AddInterchangeHeader(interchange);            
            SerializeMapping(UNHSerializer);
            SerializeMapping(BGMSerializer);

            Expression<Func<int>> x = () => 1;

            _ = x.Compile()();
            return sb.ToString();
        }

      
        private void SerializeMapping(Func<EDIInterchange, IEdifactSegment> dataElement)
        {
            var segment = dataElement(interchange);
            string ediString = segmentFormatter.Format(segment);
            sb.AppendLine(ediString);
        }

        private void AddMessageHeader(UNH uNH)
        {
            throw new NotImplementedException();
        }

        private void AddInterchangeHeader(EDIInterchange interchange)
        {
#if DEBUG
            sb.AppendLine("UNB+UNOC:3+7300015200048:14+7350000001266:14+180401:0953+964775++++0'");
#else
            throw new NotImplementedException();
#endif
        }

        private void AddServiceStringAdvice()
        {
            sb.AppendLine("UNA:+.? '");
        }

        public static string Test => "Test";
        public static Func<int, string> Test2 => (i) => "Test2";
        internal static Func<EDIInterchange, IEdifactSegment> UNHSerializer => (interchange) =>
                {
                    var unh = interchange.GetUNH();
                    return unh;
                };

        static Func<EDIInterchange, IEdifactSegment> BGMSerializer => (interchange) =>
        {
            var bgm = interchange.GetBGM();
            return bgm;
        };

    }

    public static class SegmentExtensions
    {
        public static UNH GetUNH (this EDIInterchange interchange)
        {
            return new UNH(interchange.MessageReference, interchange.MessageTypeIdentifier);
        }

        public static BGM GetBGM(this EDIInterchange interchange)
        {
            return new BGM();
        }
    }
}
