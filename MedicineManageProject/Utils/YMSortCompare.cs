using MedicineManageProject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Utils
{
    public class YMSortCompare : IComparer<SalesDataByMonthDTO>
    {
        public int Compare(SalesDataByMonthDTO x, SalesDataByMonthDTO y)
        {
            int result = y._year.CompareTo(x._year);
            if(result == 0)
            {
                return y._month.CompareTo(x._month);
            }
            return result;
        }
    }
}
