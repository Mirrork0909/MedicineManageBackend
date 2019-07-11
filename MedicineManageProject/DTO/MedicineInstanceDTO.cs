using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class MedicineInstanceDTO
    {
        public String _medicine_id { get; set; }
        public String _batch_id { get; set; }
        public Decimal _purchase_price { get; set; }
        public Decimal _sale_price { get; set; }
        public DateTime _production_date { get; set; }
        public DateTime _validity_date { get; set; }
    }
}
