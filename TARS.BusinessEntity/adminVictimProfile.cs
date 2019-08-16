using System;
using System.Collections.Generic;

namespace TARS.BusinessEntity
{
    public  class AdminVictimProfile
    {
        public List<AdminVictimProfileModel> Data { get; set; }
        public int Records { get; set; }

    }

    public class AdminVictimProfileModel
    {
        public int VictimId { get; set; }
        public Nullable<int> CityId { get; set; }
        public string Street { get; set; }
        public string IncidentType { get; set; }
        public Nullable<System.DateTime> IncidentDate { get; set; }
        public string IncidentDescription { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string HomeAddress { get; set; }
        public Nullable<bool> VictimStatus { get; set; }
        public Nullable<int> percentageProfile { get; set; }
    }
}
