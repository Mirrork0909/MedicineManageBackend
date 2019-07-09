using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Model
{
    public class SUPPLIER
    {
        [SugarColumn(IsPrimaryKey = true,IsIdentity =true)]
        public int SUPPLIER_ID { get; set; }

        public string NAME { get; set; }

        public string PHONE { get; set; }

        public int CREDIT_LEVEL { get; set; }
    }
}
