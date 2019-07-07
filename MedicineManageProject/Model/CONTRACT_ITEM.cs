using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Model
{
    public class CONTRACT_ITEM
    {
        [SugarColumn(IsPrimaryKey = true)]
        public decimal CONTRACT_ID { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public string MEDICINE_ID { get; set; }

        public decimal MEDICINE_MONEY { get; set; }

        public int MEDICINE_AMOUNT { get; set; }

        public int MEDICINE_STATUS { get; set; }
    }
}
