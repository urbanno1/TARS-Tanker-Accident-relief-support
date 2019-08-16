using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TARS.BusinessEntity
{
    public class AppFilter
    {
        public string sidx { get; set; }
        public string sord { get; set; }
        public int rows { get; set; }
        public int page { get; set; }
        public Boolean _search { get; set; }
        public Filter filters { get; set; }
        public string records { get; set; }
    }


    public class Filter
    {
        public string groupOp { get; set; }
        public IList<Rules> rules { get; set; }
    }

    public class Rules
    {
        public string field { get; set; }
        public string Op { get; set; }
        public string Data { get; set; }
    }
}
