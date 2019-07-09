using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class PurchaseDTO
    {
        public List<OrderItemDTO> _order_item_list;
        public String _staff_id;
        public String _customer_id;
  //      public Decimal _sale_price;
    }
}
