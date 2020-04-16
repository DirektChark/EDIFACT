using System;
using EDIFACT.Segments;

namespace EDIFACT.Helpers
{
    public class Interchange
    {
        public static DynamicSegment GetUNA(
            string ComponentDataElementSeparator,
            string DataElementSeparator,
            string DecimalNotation,
            string ReleaseIndicator,
            string ReservedForFutureUse,
            string SegmentTerminator)
        {

            var una = DynamicSegment.SetUNA(ComponentDataElementSeparator,
                DataElementSeparator,
                DecimalNotation,
                ReleaseIndicator,
                ReservedForFutureUse,
                SegmentTerminator);
            
            return una;
           
        }

        public static DynamicSegment GetUNB(
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
            return new Segments.DynamicSegment("UNB")
                .AddComposite(SyntaxIdentifier, SyntaxVersionNumber)
                .AddComposite(SenderIdentification, SenderIdentificationQualifier)
                .AddComposite(RecepientIdentification, RecipientIdentificationQualifier)
                .AddComposite(PreparationTimestamp.ToString("yyMMdd"), PreparationTimestamp.ToString("hhmm"))
                .AddElement(InterchangeControlReference)
                .AddElement(RecipientReference)
                .AddElement(ApplicationReference)
                .AddElement(ProcessingPriorityCode)
                .AddElement(AcknowledegementRequest)
                .AddElement(CommunicationsAgreementID)
                .AddElement(TestIndicator)
                ;
        }

        public static DynamicSegment GetUNH(
            string MessageReferenceNumber,
            string MessageTypeIdentifier,
            string MessageTypeVersionNumber,
            string MessageTypeReleaseNumber,
            string ControllingAgency,
            string AssociationAssignedCode)
        {
            return new Segments.DynamicSegment("UNH")
                .AddElement(MessageReferenceNumber)
                .AddComposite(MessageTypeIdentifier,
                    MessageTypeVersionNumber,
                    MessageTypeReleaseNumber,
                    ControllingAgency,
                    AssociationAssignedCode)
                ;
        }
    }
}
