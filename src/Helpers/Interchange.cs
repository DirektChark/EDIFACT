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
            if (string.IsNullOrWhiteSpace(SenderIdentification)) throw new ArgumentNullException(nameof(SenderIdentification));
            if (string.IsNullOrWhiteSpace(RecepientIdentification)) throw new ArgumentNullException(nameof(RecepientIdentification));
            if (string.IsNullOrWhiteSpace(InterchangeControlReference)) throw new ArgumentNullException(nameof(InterchangeControlReference));
            //if (string.IsNullOrWhiteSpace(RecipientReference)) throw new ArgumentNullException(nameof(RecipientReference));
            


            return new Segment("UNB")
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
            if (string.IsNullOrWhiteSpace(MessageReferenceNumber)) throw new ArgumentNullException(nameof(MessageReferenceNumber));



            return new Segment("UNH")
                .AddElement(MessageReferenceNumber)
                .AddComposite(MessageTypeIdentifier,
                    MessageTypeVersionNumber,
                    MessageTypeReleaseNumber,
                    ControllingAgency,
                    AssociationAssignedCode)
                ;
        }


        public static SegmentCollection AddControlElements()
        {
            throw new NotImplementedException();
        }

        public static SegmentCollection GetMessageTrailer(
            decimal ControlQuantity,
            int LineCount
            )
        {
            var cnt1 = new Segment("CNT")
                .AddComposite(1, ControlQuantity);

            var cnt2 = new Segment("CNT")
                .AddComposite(2, LineCount);
            return new SegmentCollection { cnt1, cnt2 };
        }

        public static Segment GetUNT(int SegmentCount, string MessageReferenceNumber)
        {
            if (string.IsNullOrWhiteSpace(MessageReferenceNumber)) throw new ArgumentNullException(nameof(MessageReferenceNumber));

            return new Segment("UNT")
                .AddElement(SegmentCount)
                .AddElement(MessageReferenceNumber);
        }


        public static SegmentCollection GetInterchangeFooter(
            
            
            int InterchangeControlCount, 
            string InterchangeControlReference
            )
        {
            if (InterchangeControlReference == null) throw new ArgumentNullException(nameof(InterchangeControlReference));

            var unz = new Segment("UNZ")
                .AddElement(InterchangeControlCount)
                .AddElement(InterchangeControlReference);

            return new SegmentCollection { unz };
        }

        public static Segment DefaultServiceStringAdvice => GetUNA(":", "+", ".", "?", " ", "'");
    }
}
