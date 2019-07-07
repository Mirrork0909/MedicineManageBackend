using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Model
{
    public class RETURN_RECORD
    {
        [SugarColumn(IsPrimaryKey =true)]
        public decimal RETURN_ID { get; set; }

        public decimal SALE_ID { get; set; }

        public int ORDER_ID { get; set; }

        public int BACK_NUM { get; set; }

        public string CUSTOMER_ID { get; set; }

        public string STAFF_ID { get; set; }

        public DateTime BACK_DATE { get; set; }
    }
}
