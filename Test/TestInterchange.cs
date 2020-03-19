using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Test.ESAP20.DESADV
{
    class TestInterchange
    {

        public void CreateDESADVInterchange()
        {
            string messageRef = "564535";

            var interchange = new EDIFACT.ESAP20.DESADVInterchange();
            /// UNB
            interchange.SyntaxIdentifier = "UNOC";
            interchange.SyntaxVersion = 3;
            interchange.SenderId = "7300015200048";
            interchange.SenderCodeQualifier = "14";
            interchange.RecipientId = "7350000001266";
            interchange.RecipientCodeQualifier = "14";
            interchange.DateOfPreparation = new DateTime(2018, 04, 01, 09, 53, 0);
            interchange.ControlRef = "964775";

            /// UNH
            interchange.MessageReference = "564535";
            interchange.MessageTypeIdentifier = "DESADV";
            interchange.MessageTypeVersion = "D";
            interchange.MessageReleaseNumber = "96A";
            interchange.ControllingAgencyCode = "UN";
            interchange.AssociationAssignedCode = "EAN006";


            // BGM
            dynamic  header = new ExpandoObject();
            //interchange.LevAviHuvud = header;

            header.LevAviNummer = "73000152018851234";
            header.LevAviNummer.DocumentNameCode = "351";
            header.LevAviNummer.MessageFunction = "9";
            //header.LevAviDatum = DateTime.Parse("2018-04-01");




            /// DTM 11
            header.LevAviDatum = new EDIFACT.DTM()
            {
                DateTimeQualifier = "137",
                DateTime = DateTime.Parse("2018-04-01"),
                FormatQualifier = "102"
            };

            header.SendDate = new EDIFACT.DTM
            {
                DateTimeQualifier = "11",
                DateTime = new DateTime(2018, 04, 01),
                FormatQualifier = "102"
            };

            header.DelivDate = new EDIFACT.DTM
            {
                DateTimeQualifier = "17",
                DateTime = new DateTime(2018, 04, 02),
                FormatQualifier = "102"
            };

            /// RFF ON
            header.OrderRef = "73500010009921111";
            header.OrderRef.ReferenceCode = "ON";

            header.Supplyer.SupGLN = new EDIFACT.NAD
            {
                PartyQualifier = "SU",
                PartyIdentification = "7300015200000",
                AgencyCode = "9"
            };
            header.Supplyer.SenderGLN = new EDIFACT.NAD
            {
                PartyQualifier = "DEQ",
                PartyIdentification = "7300015200024",
                AgencyCode = "9"
            };

            header.Supplyer.ShipFromGLN = new EDIFACT.NAD
            {
                PartyQualifier = "SF",
                PartyIdentification = "7300015200048",
                AgencyCode = "9"
            };

            header.Buyer = new EDIFACT.NAD
            {
                PartyQualifier = "BY",
                PartyIdentification = "7350000001204",
                AgencyCode = "9"
            };

            header.Consignee = new EDIFACT.NAD
            {
                PartyQualifier = "CN",
                PartyIdentification = "7350000001211",
                AgencyCode = "9"
            };

            header.DeliveryParty = new EDIFACT.NAD
            {
                PartyQualifier = "DP",
                PartyIdentification = "7350000001228",
                AgencyCode = "9"
            };

            header.TransportCode = new EDIFACT.TDT
            {
                TransportStageQualifier = "20",
                ModeOfTransportation = "30"
            };

            dynamic logisticInfo = new ExpandoObject();

            logisticInfo.SequenceNumber = "1";
            logisticInfo.NumberOfLogisticUnits = new EDIFACT.PAC
            {
                NumberOfPackages = "3"
            };

            logisticInfo.LogisticUnit = new
            {
                LogisticUnitSSCC = new EDIFACT.GIN
                {
                    Qualifier = "BJ",
                    IdentityNumber = "373000152000023055"
                },
                LogisticUnitCode = new EDIFACT.PAC
                {
                    PackageTypeIdentification = "CW"
                },
                PantKod = new EDIFACT.GIN
                {
                    Qualifier = "SRV",
                    IdentityNumber = "07300015200017"
                },
                GrossWeight = new EDIFACT.MEA
                {
                    Qualifier = "PD",
                    DimensionCode = "AAD",
                    UnitQualifier = "KGM",
                    Value = "500"
                }
            };


            interchange.LogisticInformation = new List<object>()
            {
                logisticInfo
            };
        }
    }
}
