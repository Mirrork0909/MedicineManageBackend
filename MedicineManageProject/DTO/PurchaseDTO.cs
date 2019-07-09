using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class PurchaseDTO
    {
        public List<OrderItemDTO> _order_item_list { get; set; }
        public String _staff_id { get; set; }
        public String _customer_id { get; set; }
        //      public Decimal _sale_price;
    }
}
