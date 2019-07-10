using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class OrderItemDTO
    {
        public String _medicine_id { get; set; }
        public String _batch_id { get; set; }
        public int _sale_num { get; set; }
        public Decimal _unit_price { get; set; }
    }
}
