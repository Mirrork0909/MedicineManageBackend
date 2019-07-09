using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class StockRecordDTO
    {
        public String _medicine_name { get; set; }
        public String _medicine_id { get; set; }
        public String _batch_id { get; set; }
        public int _amount { get; set; }
        public DateTime _time { get; set; }
        public String _reason { get; set; }
    }
}
