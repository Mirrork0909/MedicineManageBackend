using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace MedicineManageProject.Model
{
    public class EXPIRED_MEDICINE_PROCESS
    {
        [SugarColumn(IsPrimaryKey = true)]
        public Decimal STOCK_ID { get; set; }
        public String STAFF_ID { get; set; }
        public DateTime PROCESS_DATE { get; set; }
        public int PROCESS_NUMBER { get; set; }
    }
}
