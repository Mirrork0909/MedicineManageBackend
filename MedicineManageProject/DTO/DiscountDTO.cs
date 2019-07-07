using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class DiscountDTO
    {
        public Decimal _discount_id { get; set; }
        public String _medicine_id { get; set; }
        public String _medicine_name { get; set; }
        public String _batch_id { get; set; }
        public Decimal _amount { get; set; }
        public String _context { get; set; }
        public DateTime _start_time { get; set; }
        public DateTime _end_time { get; set; }
    }
}
