using EDIFACT;

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;

namespace Test.Serializer
{
    public class Serializing
    {
        EDIFACT.ESAP20.DESADVInterchange interchange = null;
        
        [SetUp]
        public void CreateInterchange()
        {
            string messageRef = "564535";

            interchange = new EDIFACT.ESAP20.DESADVInterchange();
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
            dynamic header = new ExpandoObject();
            //interchange.LevAviHuvud = header;

            header.LevAviNummer = new
            {
                GTIN = "73000152018851234",
                DocumentNameCode = "351",
                MessageFunction = "9"
            };
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

            dynamic log = new ExpandoObject();

            log.SequenceNumber = "1";
            log.NumberOfLogisticUnits = new EDIFACT.PAC
            {
                NumberOfPackages = "3"
            };

            log.LogisticUnit = new
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
                log
            };
        }


      



        [Test]
        public void CustomSerializer()
        {
            var serializer = new EDIFACT.ESAPSerializers.DESADVSerializer();
            var result = serializer.Serialize(interchange);

            var file = File.ReadAllLines(@".\TestMessages\message2.edi");

            CompareRowByRow(file, result.Split("\r\n"));
        }



        public void CompareRowByRow(string[] source1, string[] source2)
        {
            for (int i = 0; i < Math.Max(source1.Length, source2.Length); i++)
            {
                Assert.AreEqual(source1[i], source2[i]);
            }

            Assert.AreEqual(source1.Length, source2.Length);
        }
    }
}
