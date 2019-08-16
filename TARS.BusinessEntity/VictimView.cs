using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TARS.BusinessEntity
{
   public class VictimView
    {
        public List<VictimViewModel> Data { get; set; }
        public int Records { get; set; }
    }


    public class VictimViewModel
    {
        public Nullable<int> CityId { get; set; }
        public string UserId { get; set; }
        public string Street { get; set; }
        public string IncidentType { get; set; }
        public Nullable<System.DateTime> IncidentDate { get; set; }
        public string IncidentDescription { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string HomeAddress { get; set; }
        public Nullable<int> percentageProfile { get; set; }
        public int VictimId { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public System.DateTime RegisteredDate { get; set; }
        public string CityName { get; set; }
        public string LgaName { get; set; }
        public string StateName { get; set; }
        public Nullable<bool> VictimStatus { get; set; }
    }
}
