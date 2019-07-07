using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Model
{
    public class SEARCH_RECORD
    {
        [SugarColumn(IsPrimaryKey = true)]
        public decimal SEARCH_ID { get; set; }

        public string CUSTOMER_ID { get; set; }

        public DateTime SEARCH_DATE { get; set; }

        public string SEARCH_CONTENT { get; set; }
    }
}
