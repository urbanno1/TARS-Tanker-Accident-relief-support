using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TARS.BusinessEntity
{
   public class CityEntity
    {
        public IList<CityModel> Data { get; set; }
        public int Records { get; set; }
    }

    public class CityModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int LgaId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool CityStatus { get; set; }

    }
}
