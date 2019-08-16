using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TARS.BusinessEntity
{
   public class CountryEntity
    {
        public IList<CountryModel> Data { get; set; }
        public int Records { get; set; }
    }

    public class CountryModel
    {
        public int Country__Id { get; set; }
        public string Country__Code { get; set; }
        public string Country__Name { get; set; }
        public bool Country__Status { get; set; }
        public System.DateTime Created_Date { get; set; }
        public string Created_By { get; set; }
        public string Updated_By { get; set; }
        public System.DateTime Updated_Date { get; set; }
    }
}
