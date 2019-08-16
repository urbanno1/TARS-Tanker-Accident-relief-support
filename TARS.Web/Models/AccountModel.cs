using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TARS.Web.Models
{
    public class AccountModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? PercentagePhoto { get; set; }
        public int? PercentageProfile { get; set; }
        public int? PercentageGeneralPhoto { get; set; }
        public int? PercentageGeneralPercentageProfile { get; set; }
        public int? TotalGeneralPhoto { get; set; }
        public int? TotalGeneralProfile { get; set; }
        public int? TotalGeneralDonors { get; set; }
        public string LoggedOn { get; set; }

    }
}