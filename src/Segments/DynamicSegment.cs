using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EDIFACT.Segments
{

    /*
    public class Segment
    {
        string tag;
        string data;
        public Segment(string tag)
        {
            this.tag = tag;
            this.data = tag;
        }

        public Segment WithEmptyElement()
        {
            this.data += "+";
            return this;
        }
        public Segment WithElement(object data = null)
        {
            this.data += "+" + data?.ToString() ?? "";
            return this;
        }

        public override string ToString()
        {
            return data + "'";
        }

        public object WithComposite(params object[] obj)
        {
            this.data += "+" + string.Join(":", obj.Select(x => x?.ToString() ?? ""));
            return this;
        }
    }*/

    public class DynamicSegment
    {
        static ConstantExpression segmentSeparator = Expression.Constant("+");

        Expression body;

        public DynamicSegment(string tag)
        {
            this.body = Expression.Constant(tag);
        }

        public DynamicSegment AddElement(object s)
        {
            /*
            var c = Expression.Constant(s);
            MethodInfo m = typeof(string).GetMethod("Concat", new Type[] { typeof(Expression), typeof(string), typeof(string) });
            body = Expression.Call(null, m, body, segmentSeparator, c);
            */
            return AddElement(Expression.Constant(s,typeof(Object)));
        }

        public virtual DynamicSegment AddElement(Expression expr)
        {            
            MethodInfo m = typeof(string).GetMethod("Concat", new Type[] { typeof(Expression), typeof(string), typeof(string) });
            body = Expression.Call(null, m, body, segmentSeparator, expr);

            return this;
        }

        public virtual DynamicSegment AddElement()
        {
            return AddElement("");
        }

        public virtual DynamicSegment AddComposite(params object[] obj)
        {
            
            while(obj.Length > 0 && obj[obj.Length -1] == null)
            {
                obj = obj.Take(obj.Length - 1).ToArray();
            }
            if (obj.Length == 0) return AddElement();
            
            var arr = Expression.Constant(obj, typeof(object[]));
            MethodInfo m = typeof(string).GetMethod("Join", new Type[] {typeof(string), typeof(object[]) });
            var call = Expression.Call( m, Expression.Constant(":"), arr );
            return AddElement(call);
        }

        public override string ToString()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            return Helpers.SegmentHelpers.TrimSegment(Expression.Lambda<Func<string>>(body).Compile().Invoke() +"'");
        }

        public DynamicSegment AddElements(params object[] args)
        {
            foreach (object arg in args) AddElement(arg);
            return this;
        }
    }
}
