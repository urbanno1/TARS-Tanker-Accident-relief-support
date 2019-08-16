using System;
using System.Collections.Generic;

namespace TARS.BusinessEntity
{
    public class LgaEntity
    {
        public IList<LgaModel> Data { get; set; }
        public int Records { get; set; }
    }

    public class LgaModel
    {
        public int LgaId { get; set; }
        public string LgaName { get; set; }
        public int StateId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<bool> LgaStatus { get; set; }

    }
}
