using MedicineManageProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class ContractDTO
    {
        public decimal _contract_id { get; set; }

        public DateTime _delivery_date { get; set; }

        public int _contract_status { get; set; }

        public String _staff_id { get; set; }

        public DateTime _sign_date { get; set; }

        public decimal _supplier_id { get; set; }

        public String _name { get; set; }

        public String _phone { get; set; }

        public int _credit_level { get; set; }

        public List<ContractItemDTO> _contract_items { get; set; }
    }
}
