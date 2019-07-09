using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class ContractItemDTO
    {
        public decimal _contract_id { get; set; }

        public string _medicine_id { get; set; }

        public decimal _medicine_money { get; set; }

        public int _medicine_amount { get; set; }

        public int _medicine_status { get; set; }
    }
}
