using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Model
{
    [SugarTable("CONTRACT")]
    public class CONTRACT
    {
        [SugarColumn(IsPrimaryKey =true, IsIdentity = true)]
        public int CONTRACT_ID { get; set; }

        public DateTime DELIVERY_DATE { get; set; }

        public int CONTRACT_STATUS { get; set; }

        public decimal SUPPLIER_ID { get; set; }

        public string STAFF_ID { get; set; }

        public DateTime SIGN_DATE { get; set; }
    }
}
