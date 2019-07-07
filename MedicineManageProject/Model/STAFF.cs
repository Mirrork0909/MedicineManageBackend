using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace MedicineManageProject.Model
{
    public class STAFF
    {
        [SugarColumn(IsPrimaryKey = true)]
        public String STAFF_ID { get; set; }
        public String PASSWORD { get; set; }
        public String NAME { get; set; }
        public String PHONE { get; set; }
        public int POSITION { get; set; }
    }
}
