using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Model
{
    public class SUBSCRIBE
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string CUSTOMER_ID { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public string MEDICINE_ID { get; set; }
    }
}
