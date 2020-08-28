using EDIFACT.Segments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDIFACT
{
    public class EDIDocument
    {
        public Segment ServiceStringAdvice { get; set; } //UNA

        private Segment UNB;
        protected Segment UNH;
        protected List<EDIMessage> Messages;
        protected Segment UNT;
        private Segment UNZ;

        

        string SenderGLN;
        string RecipientGLN;
        DateTime PreparationTime;
        string InterchangeControlReference;

        //public abstract IEnumerable<EDIMessage> GetMessages();

        public EDIDocument()
        {
            this.Messages = new List<EDIMessage>();
        }

        public void AddInterchangeHeader(string SenderGLN,
            string RecipientGLN,
            DateTime PreparationTime,
            string InterchangeControlReference
            )
        {
            this.SenderGLN = SenderGLN;
            this.RecipientGLN = RecipientGLN;
            this.PreparationTime = PreparationTime;
            this.InterchangeControlReference = InterchangeControlReference;
        }

        public void AddMessage(EDIMessage message)
        {
#if DEBUG
            if (!message.GetType().IsSubclassOf(typeof(EDIMessage)))
                throw new InvalidOperationException("tttt");
#endif
            this.Messages.Add(message);
        }



        public SegmentCollection CreateInterchange()
        {
            SegmentCollection interchange = new SegmentCollection();
            interchange.Add(ServiceStringAdvice ?? Helpers.Interchange.DefaultServiceStringAdvice);

            bool acknowledgementRequest = false;
            Segment unb = EDIFACT.Helpers.Interchange.GetUNB("UNOC", 3, SenderGLN, "14",
               RecipientGLN, "14", PreparationTime,
               InterchangeControlReference,
               null,
               null,
               null,
               acknowledgementRequest ? 1 : (Nullable<int>)null,
               null,
               null);
            interchange.Add(unb);

            foreach(var message in Messages)
            {
                interchange.AddSegments(message.CreateMessage());
            }

            int InterchangeControlCount = 1;

            var sg = Helpers.Interchange.GetInterchangeFooter(
                InterchangeControlCount,
                this.InterchangeControlReference);
            interchange.AddSegments(sg);
            return interchange;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var seg in CreateInterchange()) sb.AppendLine(seg.ToString());
            return sb.ToString();
        }
    }



}
