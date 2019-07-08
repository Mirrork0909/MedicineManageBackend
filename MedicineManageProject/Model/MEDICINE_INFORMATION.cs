using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace MedicineManageProject.Model
{
    public class MEDICINE_INFORMATION
    {
        [SugarColumn(IsPrimaryKey = true)]
        public String MEDICINE_ID { set; get; }
        public String MEDICINE_NAME { set; get; }
        public String MEDICINE_TYPE { set; get; }
        public String MEDICINE_INGREDIENTS { set; get; }
        public String MEDICINE_CHARACTER { set; get; }
        public String MEDICINE_APPLICABILITY { set; get; }
        public String MEDICINE_USAGE { set; get; }
        public String MEDICINE_ATTENTION { set; get; }
        
    }
}
