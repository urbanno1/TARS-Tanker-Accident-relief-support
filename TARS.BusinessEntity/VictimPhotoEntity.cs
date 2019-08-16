using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TARS.BusinessEntity
{

    public class VictimPhotoEntity
    {
        public IList<VictimPhotoModel> Data { get; set; }
        public int Records { get; set; }
    }
        public class VictimPhotoModel
    {
        public int PhotoId { get; set; }
        public string PhotoTitle { get; set; }
        public string PhotoUrl { get; set; }
        public int VictimId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }

    }
}
