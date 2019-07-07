using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace MedicineManageProject.Model
{
    public class SET_DISCOUNT
    {
        [SugarColumn(IsPrimaryKey = true)]
        public Decimal DISCOUNT_ID { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public String MEDICINE_ID { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public String BATCH_ID { get; set; }
    }
}
