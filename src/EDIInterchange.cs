using EDIFACT;
using System;

namespace EDIFACT
{
    public class EDIInterchange
    {
        [DataElement("UNB/0")]
        public string SyntaxIdentifier { get; set; }

        [DataElement("UNB/0/1", Mandatory = true)]
        public int SyntaxVersion { get; set; }

        [DataElement("UNB/1/0", Mandatory = true)]
        public string SenderId { get; set; }
        
        [DataElement("UNB/1/1", Mandatory = true)]
        public string SenderCodeQualifier { get; set; }
        
        [DataElement("UNB/1/2", Mandatory = false)]
        public string ReverseRoutingAddress { get; set; }

        [DataElement("UNB/2/0", Mandatory = true)]
        public string RecipientId { get; set; }

        [DataElement("UNB/2/1", Mandatory = true)]
        public string RecipientCodeQualifier { get; set; }

        [DataElement("UNB/2/2", Mandatory = false)]
        public string RoutingAddress { get; set; }

        [DataElement("3/0", Format = "yyMMdd")]
        [DataElement("3/1", Format = "HHmm")]
        public DateTime DateOfPreparation { get; set; }

        [DataElement("UNB/4", Mandatory = true)]
        public string ControlRef { get; set; }

        [DataElement("UNB/8")]
        public int AckRequest { get; set; }

        [DataElement("UNH/0")]
        public string MessageReference { get; set; }

        [DataElement("UNH/1")]
        public string MessageTypeIdentifier { get; set; }

        [DataElement("UNH/1/1")]
        public string MessageTypeVersion { get; set; }
        
        [DataElement("UNH/1/2")]
        public string MessageReleaseNumber { get; set; }

        [DataElement("UNH/1/3")]
        public string ControllingAgencyCode { get; set; }
        
        [DataElement( "UNH/1/4")]
        public string AssociationAssignedCode { get; set; }

        [DataElement( "UNZ/0")]
        public int TrailerControlCount { get; set; }

        [DataElement("UNZ/1")]
        public string TrailerControlReference { get; set; }
    }

}
