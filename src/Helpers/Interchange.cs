using System;
using EDIFACT.Segments;

namespace EDIFACT.Helpers
{
    public class Interchange
    {
        public static Segment GetUNA(
            string ComponentDataElementSeparator,
            string DataElementSeparator,
            string DecimalNotation,
            string ReleaseIndicator,
            string ReservedForFutureUse,
            string SegmentTerminator)
        {

            var una = Segment.SetUNA(ComponentDataElementSeparator,
                DataElementSeparator,
                DecimalNotation,
                ReleaseIndicator,
                ReservedForFutureUse,
                SegmentTerminator);
            
            return una;
           
        }

        public static Segment GetUNB(
            string SyntaxIdentifier,
            int SyntaxVersionNumber,
            string SenderIdentification,
            string SenderIdentificationQualifier,
            string RecepientIdentification,
            string RecipientIdentificationQualifier,
            DateTime PreparationTimestamp,
            string InterchangeControlReference,
            string RecipientReference,
            string ApplicationReference,
            string ProcessingPriorityCode = null,
            int? AcknowledegementRequest = null,
            string CommunicationsAgreementID = null,
            int? TestIndicator = null
            )
        {
            return new Segments.Segment("UNB")
                .AddComposite(SyntaxIdentifier, SyntaxVersionNumber)
                .AddComposite(SenderIdentification, SenderIdentificationQualifier)
                .AddComposite(RecepientIdentification, RecipientIdentificationQualifier)
                .AddComposite(PreparationTimestamp.ToString("yyMMdd"), PreparationTimestamp.ToString("hhmm"))
                .AddElement(InterchangeControlReference)
                .AddElement(RecipientReference, allowNull: true)
                .AddElement(ApplicationReference, allowNull: true)
                .AddElement((object)ProcessingPriorityCode ?? DataNull.Value)
                .AddElement((object)AcknowledegementRequest ?? DataNull.Value)
                .AddElement((object)CommunicationsAgreementID ?? DataNull.Value)
                .AddElement(TestIndicator.HasValue ? (object)TestIndicator.Value : DataNull.Value)
                ;
        }

        public static Segment GetUNH(
            string MessageReferenceNumber,
            string MessageTypeIdentifier,
            string MessageTypeVersionNumber,
            string MessageTypeReleaseNumber,
            string ControllingAgency,
            string AssociationAssignedCode)
        {
            return new Segments.Segment("UNH")
                .AddElement(MessageReferenceNumber)
                .AddComposite(MessageTypeIdentifier,
                    MessageTypeVersionNumber,
                    MessageTypeReleaseNumber,
                    ControllingAgency,
                    AssociationAssignedCode)
                ;
        }

        public static SegmentCollection GetInterchangeHeader(string SenderGLN,
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
            bool acknowledgementRequest = false;

            Segment una = EDIFACT.Helpers.Interchange.GetUNA(":", "+", ".", "?", " ", "'");

            Segment unb = EDIFACT.Helpers.Interchange.GetUNB("UNOC", 3, SenderGLN, "14",
                RecipientGLN, "14", PreparationTime,
                InterchangeControlReference,
                null,
                null,
                null,
                acknowledgementRequest ? 1 : (Nullable<int>)null,
                null,
                null);

            Segment unh = EDIFACT.Helpers.Interchange.GetUNH(
                MessageReferenceNumber,
                MessageTypeIdentifier,
                MessageTypeVersionNumber,
                MessageTypeReleaseNumber,
                ControllingAgency,
                AssociationAssignedCode);

            return new SegmentCollection { una, unb, unh };
        }


        public static SegmentCollection GetInterchangeFooter(
            decimal ControlQuantity, 
            int LineCount, 
            int SegmentCount, 
            string MessageReferenceNumber,
            int InterchangeControlCount, 
            string InterchangeControlReference
            )
        {
            
            if (MessageReferenceNumber == null) throw new ArgumentNullException(nameof(MessageReferenceNumber));
            if (InterchangeControlReference == null) throw new ArgumentNullException(nameof(InterchangeControlReference));

            var cnt1 = new Segment("CNT")
                .AddComposite(1, ControlQuantity);

            var cnt2 = new Segment("CNT")
                .AddComposite(2, LineCount);

            var unt = new Segment("UNT")
                .AddElement(SegmentCount)
                .AddElement(MessageReferenceNumber);

            var unz = new Segment("UNZ")
                .AddElement(InterchangeControlCount)
                .AddElement(InterchangeControlReference);

            return new SegmentCollection { cnt1, cnt2, unt, unz };

        }
    }
}
