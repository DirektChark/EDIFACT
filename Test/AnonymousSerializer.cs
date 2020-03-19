using EDIFACT;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Test.Serializer
{
    class AnonymousSerializer
    {
        [Test]
        public void UNH()
        {
            string fact = "UNH+564535+DESADV:D:96A:UN:EAN006'";

            var o = new
            {
                Tag = "UNH",
                MessageReferenceNumber = "564535",
                MessageIdentifier = "DESADV:D:96A:UN:EAN006"
            };

            var o2 = new
            {
                Tag = "UNH",
                reff = 564535,
                MessageIdentifier = new[] { "DESADV", "D", "96A", "UN", "EAN006" }
            };

            string s1 = EDIFACT.EdiObject.SerializeObject(o);
            string s2 = EdiObject.SerializeObject(o2);

            Assert.AreEqual(fact, s1);
            Assert.AreEqual(fact, s2);
        }

        [Test]
        public void Message()
        {
            var message = new List<object>();

            message.Add("UNA:+.? '");
            message.Add(new
            {
                Tag = "UNB",
                c1 = "UNOC:3",
                Msg = "7300015200048:14",
                ret = "7350000001266:14",
                Dt = new DateTime(2018, 4, 1, 9, 53, 0).ToString("yyMMdd:hhmm"),
                reff = 964775,
                padding = new DataElementPadding(2),
                TestIndicator = 0
            });

            message.Add(new
            {
                head = "UNH",
                id = 564535,
                typeId = new[] { "DESADV", "D", "96A", "UN", "EAN006" }
            });

            message.Add(EdiObject.SerializeObject(new
            {
                t = "BGM",
                messageCode = 351
            }));

            message.Add(new {o= "DTM" });
            message.Add("DTM");
            message.Add("DTM");

            var file = File.ReadAllLines(@".\TestMessages\message2.edi");

            CompareRowByRow(file, message.Select(x=> EdiObject.SerializeObject(x)).ToArray());
        }

        public void CompareRowByRow(string[] source1, string[] source2)
        {
            for (int i = 0; i < Math.Min(source1.Length, source2.Length); i++)
            {
                Assert.AreEqual(source1[i], source2[i]);
            }

            Assert.AreEqual(source1.Length, source2.Length, "Message lengt does not match test message.");
        }
    }
}
