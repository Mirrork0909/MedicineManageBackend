using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Model
{
    public class SALE_RECORD
    {
        [SugarColumn(IsPrimaryKey =true)]
        public decimal SALE_ID { get; set; }

        public string CUSTOMER_ID { get; set; }

        public string STAFF_ID { get; set; }

        public DateTime SALE_DATE { get; set; }

        public decimal SALE_PRICE { get; set; }
    }
}
