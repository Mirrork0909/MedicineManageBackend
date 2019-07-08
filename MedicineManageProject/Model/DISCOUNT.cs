using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace MedicineManageProject.Model
{
    public class DISCOUNT
    {
        [SugarColumn(IsPrimaryKey = true,IsIdentity = true)]
        public int DISCOUNT_ID { get; set; }

        public Decimal DISCOUNT_PRICE { get; set; }
        public DateTime START_TIME { get; set; }
        public DateTime END_TIME { get; set; }
        public String CONTEXT { get; set; }
    }
}
