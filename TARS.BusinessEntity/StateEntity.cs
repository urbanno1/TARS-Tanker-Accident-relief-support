using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TARS.BusinessEntity
{
    public class StateEntity
    {
        public IList<StateModel> Data { get; set; }
        public int Records { get; set; }
    }


    public class StateModel
    {
        public int StateId { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int CountryId { get; set; }
        public Nullable<bool> StateStatus { get; set; }
    }
}
