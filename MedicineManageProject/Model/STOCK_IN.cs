using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Model
{
    public class STOCK_IN
    {
        [SugarColumn(IsPrimaryKey = true)]
        public decimal STOCK_ID { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public decimal CONTRACT_ID { get; set; }

        public DateTime IN_TIME { get; set; }

        public int IN_NUM { get; set; }
    }
}
