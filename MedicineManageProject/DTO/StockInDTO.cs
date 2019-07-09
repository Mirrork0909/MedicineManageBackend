using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class StockInDTO
    {
        public String medicine_id { set; get; }

        public String batch_id { get; set; }

        public DateTime in_time { get; set; }

        public int in_num { get; set; }
    }
}
