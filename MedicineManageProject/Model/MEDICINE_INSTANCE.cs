using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace MedicineManageProject.Model
{
    public class MEDICINE_INSTANCE
    {
        [SugarColumn(IsPrimaryKey = true)]
        public String MEDICINE_ID { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public String BATCH_ID { get; set; }
        public Decimal PURCHASE_PRICE { get; set; }
        public Decimal SALE_PRICE { get; set; }
        public Decimal SUPPLIER_ID { get; set; }
        public DateTime PRODUCTION_DATE { get; set; }
        public DateTime VALIDITY_DATE { get; set; }
        public String MEDICINE_IMAGE { set; get; }
    }
}
