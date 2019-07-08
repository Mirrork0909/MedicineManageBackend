using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class SaleInformationDTO
    {
        public decimal _sale_id { get; set; }

        public string _customer_id { get; set; }

        public string _staff_id { get; set; }

        public DateTime _sale_date { get; set; }

        public decimal _sale_price { get; set; }
    }
}
