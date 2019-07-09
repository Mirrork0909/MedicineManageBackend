using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class StockInDTO
    {
        public String _medicine_id { set; get; }

        public decimal _stock_in { get; set; }

        public DateTime _in_time { get; set; }

        public int _in_num { get; set; }
    }
}
