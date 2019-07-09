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

    public class YMSortCompare : IComparer<CostByMonthDTO>
    {
        public int Compare(CostByMonthDTO x, CostByMonthDTO y)
        {
            int result = y._year.CompareTo(x._year);
            if (result == 0)
            {
                return y._month.CompareTo(x._month);
            }
            return result;
        }
    }

}
