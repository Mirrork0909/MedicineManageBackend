using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Model
{
    public class CUSTOMER
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string CUSTOMER_ID { get; set; }

        public string PASSWORD { get; set; }

        public string NAME { get; set; }

        public string PHONE { get; set; }

        public DateTime SIGN_DATE { get; set; }

        public int BONUS_POINT { get; set; }
    }
}
