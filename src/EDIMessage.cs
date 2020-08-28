using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDIFACT
{
    public class EDIMessage : SegmentCollection
    {
        public string MessageReferenceNumber { get; set; }
        string MessageTypeIdentifier;
        string MessageTypeVersionNumber;
        string MessageTypeReleaseNumber;
        string ControllingAgency;
        string AssociationAssignedCode;
        public void SetMessageHeder(string MessageReferenceNumber,
            string MessageTypeIdentifier,
                string MessageTypeVersionNumber,
                string MessageTypeReleaseNumber,
                string ControllingAgency,
                string AssociationAssignedCode)
        {
            this.MessageReferenceNumber = MessageReferenceNumber;
            this.MessageTypeIdentifier = MessageTypeIdentifier;
            this.MessageTypeVersionNumber = MessageTypeVersionNumber;
            this.MessageTypeReleaseNumber = MessageTypeReleaseNumber;
            this.ControllingAgency = ControllingAgency;
            this.AssociationAssignedCode = AssociationAssignedCode;
        }

        public virtual IEnumerable<Segment> CreateMessage()
        {
            return CreateMessage(this);
        }

        protected virtual IEnumerable<Segment> CreateMessage(IEnumerable<Segment> segments)
        {
            if (string.IsNullOrWhiteSpace(MessageReferenceNumber)) throw new InvalidOperationException(nameof(MessageReferenceNumber));
            if (string.IsNullOrWhiteSpace(MessageTypeIdentifier)) throw new InvalidOperationException(nameof(MessageTypeIdentifier));
            if (string.IsNullOrWhiteSpace(MessageTypeReleaseNumber)) throw new InvalidOperationException(nameof(MessageTypeReleaseNumber));

            yield return GetUNH();
            foreach (Segment s in segments) yield return s;
            yield return GetUNT(segments);

        }

        public Segment GetUNH()
        {
            return Helpers.Interchange.GetUNH(MessageReferenceNumber,
                MessageTypeIdentifier,
                MessageTypeVersionNumber,
                MessageTypeReleaseNumber,
                ControllingAgency,
                AssociationAssignedCode);
        }

        public Segment GetUNT(IEnumerable<Segment> segments)
        {
            int segmentCount = segments.Count() + 2; //count of detail lines + unh and unt
            return Helpers.Interchange.GetUNT(segmentCount, MessageReferenceNumber);
        }
    }
}
