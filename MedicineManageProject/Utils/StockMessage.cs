using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Utils
{
    public class StockMessage
    {
        public static String EXPIRED_MEDICINE = "过期药品处理";
        public static String PROBLEM_MEDICINE = "问题药品退回";
        public static String SOLED_OUT = "药品售出";

        public static String BACK_IN = "顾客退货药品";
        public static String STOCK_IN = "合同购入药品";
    }
}
