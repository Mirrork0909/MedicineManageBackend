using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace MedicineManageProject.Model
{
    public class MEDICINE_STOCK
    {
        [SugarColumn(IsPrimaryKey = true)]
        public Decimal STOCK_ID { get; set; }
        public String MEDICINE_ID { get; set; }
        public String BATCH_ID { set; get; }
        public int AMOUNT { get; set; }
    }
}
