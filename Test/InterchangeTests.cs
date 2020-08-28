using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edifact_Test
{
    public class InterchangeTests
    {
        /*
        [Test]
        public void EDIInterchangeFooter()
        {
            var footer = EDIFACT.Helpers.Interchange.GetInterchangeFooter(62, 2, 35, "564535", 1, "964775")
                .Select(x => x.ToString()).ToList();

            Assert.AreEqual("CNT+1:62'", footer[0]);
            Assert.AreEqual("CNT+2:2'", footer[1]);
            Assert.AreEqual("UNT+35+564535'", footer[2]);
            Assert.AreEqual("UNZ+1+964775'", footer[3]);
        }*/
    }
}
