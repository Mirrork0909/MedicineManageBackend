using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Model
{
    public class PROBLEM_MEDICINE_RETURN
    {
        [SugarColumn(IsPrimaryKey =true)]
        public decimal STOCK_ID { get; set; }

        [SugarColumn(IsPrimaryKey =true)]
        public decimal CONTRACT_ID { get; set; }

        public string STAFF_ID { get; set; }

        public DateTime RETURN_DATE { get; set; }

        public int RETURN_NUMBER { get; set; }
    }
}
