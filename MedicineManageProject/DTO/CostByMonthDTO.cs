using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{

    public class CostByMonthDTO
    {
        public int _year { get; set; }
        public int _month { get; set; }
        public decimal _cost { get; set; }
    }

}
