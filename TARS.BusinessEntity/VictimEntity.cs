using System;
using System.Collections.Generic;

namespace TARS.BusinessEntity
{
    public class VictimEntity
    {
        public VictimMod Data { get; set; }
        public int Records { get; set; }
    }

    public class VictimMod
    {
        public StateModel State { get; set; }
        public LgaModel Lga { get; set; }
        public CityModel City { get; set; }
        public VictimModel Victim { get; set; }
    }

    public class VictimModel
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
