using EDIFACT.Segments;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edifact_Test
{
    public class ExpressionVisitorTest
    {

        [Test]
        public void GetFirstElementValue()
        {
            var segment = new Segment("TAG")
                .AddElement("first");

            object o = segment[1];

            Assert.AreEqual("first", o.ToString());
        }

        [Test]
        public void GetFirstOfManny()
        {
            var segment = new Segment("TAG")
               .AddElement("first")
               .AddElement("second")
               .AddElement("third");

            object o = segment[1];

            Assert.AreEqual("first", o.ToString());
        }

        [Test]
        public void GetSecond()
        {
            var segment = new Segment("TAG")
               .AddElement("first")
               .AddElement("second");

            object o = segment[1];

            Assert.AreEqual("second", o.ToString());
        }

        [Test]
        public void GetSecondOfManny()
        {
            var segment = new Segment("TAG")
               .AddElement("first")
               .AddElement("second")
               .AddElement("third");

            object o = segment[1];

            Assert.AreEqual("second", o.ToString());
        }
    }
}
