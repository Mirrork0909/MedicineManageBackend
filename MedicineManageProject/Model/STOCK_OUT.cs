using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Model
{
    public class STOCK_OUT
    {
        [SugarColumn(IsPrimaryKey = true)]
        public decimal STOCK_ID { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public decimal SALE_ID { get; set; }

        public string STAFF_ID { get; set; }

        public DateTime OUT_TIME { get; set; }

        public int OUT_NUM { get; set; }
    }
}
