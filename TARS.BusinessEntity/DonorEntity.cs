using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TARS.BusinessEntity
{
    public class DonorEntity
    {
        public List<DonorEntityModel> Data { get; set; }
        public int Records { get; set; }

    }

    public class DonorEntityModel
    {
        public int DonorId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankName { get; set; }
        public string DonorFor { get; set; }
        public string DonatingFor { get; set; }
        public decimal TransationAmount { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string TransactionId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
