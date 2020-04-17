using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDIFACT;
using EDIFACT.Segments;

namespace EDIFACT.ESAP20
{
    public static class ESAP20TermMapper
    {
        public static Dictionary<string, object> Defaults { get; } = new Dictionary<string, object>();

        public static void AddUNA(this EDIFACT.SegmentCollection doc, object obj) { }
        public static void AddUNB(this EDIFACT.SegmentCollection doc, object obj) { }
        public static void AddUNH(this EDIFACT.SegmentCollection doc, object obj) { }

        public static void AddBGM(this EDIFACT.SegmentCollection doc) { }



        public static LIN AddLIN(this SegmentCollection doc)
        {
            var l = new LIN();
            doc.Add(l);
            return l;
        }


        private static object GetDataElement(string elementId)
        {
            return null;
        }

        public  static Dictionary<string,object> GetElementData(object dto, params string[] args)
        {
            Dictionary<string, object> list = new Dictionary<string, object>();

            Type t = dto.GetType();
            var props = t.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(TermAttribute)));

            foreach (var arg in args)
            {
                object o;
                try
                {
                    o = props.Where(p =>
                    {
                        var a = p.GetCustomAttributes(typeof(TermAttribute), false).Single() as TermAttribute;
                        return a.Term == arg;
                    }).Single().GetValue(dto);
                    
                    if(o == null)
                    {
                        Defaults.TryGetValue(arg, out o);
                    }
                    list.Add(arg, o);

                }
                catch(InvalidOperationException e)
                {
                    Defaults.TryGetValue(arg, out o);

                    list.Add(arg, o);
                }

                
            }

            return list;
        }
    }
}
