using EDIFACT.Segments;
using NUnit.Framework;
using System;

namespace Edifact_Test
{
    public class FluentSegmentTests
    {
        [Test]
        public void CPS()
        {
            var cps = new DynamicSegment("CPS")
                .AddElement("2");
            Assert.AreEqual("CPS+2'", cps.ToString());
        }

        [Test]
        public void PAC()
        {
            var cps = new DynamicSegment("PAC")
                .AddElement()
                .AddElement()
                .AddElement("CW");
            Assert.AreEqual("PAC+++CW'", cps.ToString());
        }

        [Test]
        public void MEA()
        {
            var cps = new DynamicSegment("MEA")
                .AddElement("PD")
                .AddElement("AAD")
                .AddComposite("KGM", 500);

            Assert.AreEqual("MEA+PD+AAD+KGM:500'", cps.ToString());
        }

        [Test]
        public void PCI()
        {
            var cps = new DynamicSegment("PCI")
                .AddElement("39");
            Assert.AreEqual("PCI+39'", cps.ToString());
        }

        [Test]
        public void GIN()
        {
            var cps = new DynamicSegment("GIN")
                .AddElement("SRV")
                .AddElement("07300015200017");
            Assert.AreEqual("GIN+SRV+07300015200017'", cps.ToString());
        }

        [Test]
        public void LIN()
        {
            var cps = new DynamicSegment("LIN")
                .AddElement("1")
                .AddElement()
                .AddComposite("07300015200154", "SRV");
            Assert.AreEqual("LIN+1++07300015200154:SRV'", cps.ToString());
        }

        [Test]
        public void PIA_SA()
        {
            var cps = new DynamicSegment("PIA")
                .AddElement("1")
                .AddComposite(1250, "SA");
            Assert.AreEqual("PIA+1+1250:SA'", cps.ToString());
        }

        [Test]
        public void PIA_NB()
        {
            var cps = new DynamicSegment("PIA")
                .AddElement("1").AddComposite("AB152715", "NB");

            Assert.AreEqual("PIA+1+AB152715:NB'", cps.ToString());
        }

        [Test]
        public void PIA_SN()
        {
            var cps = new DynamicSegment("PIA")
                .AddElement("1")
                .AddComposite("878498987656", "SN");
            Assert.AreEqual("PIA+1+878498987656:SN'", cps.ToString());
        }

        [Test]
        public void PIA_SRV()
        {
            var cps = new DynamicSegment("PIA")
                .AddElement("4")
                .AddComposite("07300015200161", "SRV");
            Assert.AreEqual("PIA+4+07300015200161:SRV'", cps.ToString());
        }

        [Test]
        public void PIA_4SA()
        {
            var cps = new DynamicSegment("PIA")
                .AddElement("4")
                .AddComposite(8954, "SA");
            Assert.AreEqual("PIA+4+8954:SA'", cps.ToString());
        }



        [Test]
        public void MEA_PD()
        {
            var cps = new DynamicSegment("MEA")
                .AddElement("PD")
                .AddElement("AAC")
                .AddComposite("KGM", 200);
            Assert.AreEqual("MEA+PD+AAC+KGM:200'", cps.ToString());
        }

        [Test]
        public void QTY()
        {
            var cps = new DynamicSegment("QTY")
                .AddComposite("12", "50");
            Assert.AreEqual("QTY+12:50'", cps.ToString());
        }

        [Test]
        public void DTM_361()
        {
            var cps = new DTM()
                .WithDateformat("yyyyMMdd")
                .AddComposite(361, new DateTime(2020, 12, 24), 102);
            Assert.AreEqual("DTM+361:20201224:102'", cps.ToString());
        }

        [Test]
        public void DTM_NullComponent()
        {
            var cps = new DTM()
                .WithDateformat("yyyyMMdd")
                .AddComposite(361, null, 102);
            Assert.AreEqual("DTM+361::102'", cps.ToString());
        }

        [Test]
        public void DTM_36()
        {
            var cps = new DynamicSegment("DTM")
                .AddComposite(36, new DateTime(2020, 12, 24).ToString("yyyyMMdd"), 102);

            Assert.AreEqual("DTM+36:20201224:102'", cps.ToString());
        }

        [Test]
        public void RFF()
        {
            var cps = new DynamicSegment("RFF")
                .AddComposite("ON", 73500010009921111, 10);
            Assert.AreEqual("RFF+ON:73500010009921111:10'", cps.ToString());
        }

        [Test]
        public void QVR()
        {
            var cps = new DynamicSegment("QVR")
                .AddComposite(-5, 21)
                .AddElements("CP", "AV");
            Assert.AreEqual("QVR+-5:21+CP+AV'", cps.ToString());
        }

        [Test]
        public void DTM_64()
        {
            var cps = new DynamicSegment("DTM")
                .AddComposite(64, new DateTime(2018, 04, 03).ToString("yyyyMMdd"), 102);
            Assert.AreEqual("DTM+64:20180403:102'", cps.ToString());
        }

        [Test]
        public void QVR_9()
        {
            var cps = new DynamicSegment("QVR")
                .AddComposite(9, "21")
                .AddElement("AC")
                .AddElement("PC");
            Assert.AreEqual("QVR+9:21+AC+PC'", cps.ToString());
        }

        [Test]
        public void CNT_1()
        {
            var cps = new DynamicSegment("CNT")
                .AddComposite(1, 62);
            Assert.AreEqual("CNT+1:62'", cps.ToString());
        }

        [Test]
        public void CNT_2()
        {
            var cps = new DynamicSegment("CNT")
                .AddComposite(2, 2);
            Assert.AreEqual("CNT+2:2'", cps.ToString());
        }

        [Test]
        public void UNT()
        {
            var cps = new DynamicSegment("UNT")
                .AddElements(35, 564535);
            Assert.AreEqual("UNT+35+564535'", cps.ToString());
        }

        [Test]
        public void UNZ()
        {
            var cps = new DynamicSegment("UNZ")
                .AddElement("1")
                .AddElement(964775);
            Assert.AreEqual("UNZ+1+964775'", cps.ToString());
        }

        [Test]
        public void NAD()
        {
            var cps = new DynamicSegment("NAD")
                .AddElement("DEQ")
                .AddComposite("7300015200024", null, 9);
            Assert.AreEqual("NAD+DEQ+7300015200024::9'", cps.ToString());
        }

      

    }
}
