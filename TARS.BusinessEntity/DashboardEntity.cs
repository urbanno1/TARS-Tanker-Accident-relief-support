using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TARS.BusinessEntity
{
   public class DashboardEntity
    {
        public int? PercentageProfile { get;  set; }
        public int? PercentagePhoto { get;  set; }
        public int? PercentageGeneralPhoto { get;  set; }
        public int? PercentageGeneralPercentageProfile { get;  set; }
        public int? TotalGeneralPhoto { get; set; }
        public int? TotalGeneralProfile { get; set; }
        public int? TotalGeneralDonors { get; set; }

    }
}
