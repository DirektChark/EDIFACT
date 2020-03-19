using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EDIFACT
{
    public class EdiObject
    {
        public static string SerializeObject(object o)
        {
            var anonType = o.GetType();
            var props = anonType.GetProperties();

            string tag = anonType.GetProperty("Tag")?.GetValue(o).ToString() ?? props.First().GetValue(o).ToString();

            List<string> elements = new List<string>();

            foreach (var prop in props.Skip(1))
            {
                object val = prop.GetValue(o);

                if (val is string s) {
                    if (s == tag) continue;
                    elements.Add(s); }
                else if (val is Array) elements.Add(SerializeComposite(val));
                else elements.Add(val.ToString());

            }

            return $"{tag}+{string.Join("+",elements)}'";
        }

        private static string SerializeComposite(object val)
        {
            IEnumerable<object> subelements = val as IEnumerable<object>;
            return string.Join(":", subelements.Select(x => x.ToString()));
        }
    }
}
