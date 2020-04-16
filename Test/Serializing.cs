using EDIFACT;
using EDIFACT.Segments;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;

namespace Test.Serializer
{
    public class Serializing
    {
       

        [Test]
        public void DynamicSegmentToSTring()
        {
            var ds = new DynamicSegment("TEST").AddElement("D");
            Assert.AreEqual("TEST+D'", ds.ToString());
        }

       

       

       

        [Test]
        public void DynamicSegmentToSTringWhenEnumerating()
        {
            List<DynamicSegment> list = new List<DynamicSegment>();
            var ds = new DynamicSegment("TEST").AddElement("D");
            list.Add(ds);

            Assert.AreEqual("TEST+D'", list.First().ToString());
        }

        [Test]
        public void DynamicSegmentLinqCast()
        {
            List<DynamicSegment> list = new List<DynamicSegment>();
            var ds = new DynamicSegment("TEST").AddElement("D");
            list.Add(ds);
            var list2 = list.Select(x => x.ToString());
            Assert.AreEqual("TEST+D'", list2.First());
        }

    }
}
