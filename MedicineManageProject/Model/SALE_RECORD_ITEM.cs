using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Model
{
    public class SALE_RECORD_ITEM
    {
        [SugarColumn(IsPrimaryKey =true)]
        public decimal SALE_ID { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public int ORDER_ID { get; set; }

        public string MEDICINE_ID { get; set; }

        public string BATCH_ID { get; set; }

        public int SALE_NUM { get; set; }

        public decimal UNIT_PRICE { get; set; }
    }
}
