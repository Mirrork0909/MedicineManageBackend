using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Model
{
    [SugarTable("FEEDBACK")]
    public class FEEDBACK
    {
        [SugarColumn(IsPrimaryKey =true)]
        public decimal SUGGEST_ID { get; set; }

        public string CUSTOMER_ID { get; set; }

        public DateTime SUGGEST_DATE { get; set; }

        public string SUGGEST_CONTENT { get; set; }
    }
}
