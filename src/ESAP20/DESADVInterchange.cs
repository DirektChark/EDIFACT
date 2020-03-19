using System;
using System.Collections.Generic;
using System.Text;

namespace EDIFACT.ESAP20
{
    public class DESADVInterchange : EDIInterchange
    {
        public IEnumerable<object> LogisticInformation { get; set; }
    }
}
