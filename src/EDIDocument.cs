using EDIFACT.Segments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDIFACT
{
    public class EDIDocument : SegmentCollection
    {
        string SenderGLN;
        string RecipientGLN;
        DateTime PreparationTime;
        string InterchangeControlReference;
        string MessageReferenceNumber;
        string MessageTypeIdentifier;
        string MessageTypeVersionNumber;
        string MessageTypeReleaseNumber;
        string ControllingAgency;
        string AssociationAssignedCode;

        /*
        public void AddSegments(SegmentCollection segmentGroup)
        {
            AddSegments((IEnumerable<Segment>)segmentGroup);
        }*/



        public SegmentCollection AppendControlSegment()
        {

            decimal ControlQuantity = segments.Where(s => s.Tag == "QTY").Sum(qty => Helpers.SegmentHelpers.GetQtyValue(qty));
            int LineCount = this.segments.Where(s => s.Tag == "LIN").Count();
            int SegmentCount = this.segments.Count;
            
            for(int i = SegmentCount-1; i > 0; --i)
            {
                if(segments[i].Tag == "BGM")
                {
                    SegmentCount -= i;
                    break;
                }
            }

            int InterchangeControlCount = 1;

            var sg = Helpers.Interchange.GetInterchangeFooter(ControlQuantity,
                LineCount,
                SegmentCount+4,
                MessageReferenceNumber,
                InterchangeControlCount,
                this.InterchangeControlReference);

            segments.AddRange(sg);
            return sg;
        }

        public void AddInterchangeHeader(string SenderGLN,
            string RecipientGLN,
            DateTime PreparationTime,
            string InterchangeControlReference,
            string MessageReferenceNumber,
            string MessageTypeIdentifier,
                string MessageTypeVersionNumber,
                string MessageTypeReleaseNumber,
                string ControllingAgency,
                string AssociationAssignedCode)
        {
            this.SenderGLN = SenderGLN;
            this.RecipientGLN = RecipientGLN;
            this.PreparationTime = PreparationTime;
            this.InterchangeControlReference = InterchangeControlReference;
            this.MessageReferenceNumber = MessageReferenceNumber;
            this.MessageTypeIdentifier = MessageTypeIdentifier;
            this.MessageTypeVersionNumber = MessageTypeVersionNumber;
            this.MessageTypeReleaseNumber = MessageTypeReleaseNumber;
            this.ControllingAgency = ControllingAgency;
            this.AssociationAssignedCode = AssociationAssignedCode;


            var sg = Helpers.Interchange.GetInterchangeHeader(SenderGLN,
            RecipientGLN,
             PreparationTime,
            InterchangeControlReference,
            MessageReferenceNumber,
            MessageTypeIdentifier,
            MessageTypeVersionNumber,
            MessageTypeReleaseNumber,
            ControllingAgency,
            AssociationAssignedCode);

            this.AddSegments(sg);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var seg in this) sb.AppendLine(seg.ToString());
            return sb.ToString();
        }
    }
}
