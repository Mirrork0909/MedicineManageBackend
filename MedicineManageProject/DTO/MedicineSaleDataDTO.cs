using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class MedicineSaleDataDTO:IComparable
    {
        public String _medicine_id { set; get; }
        public String _medicine_name { set; get; }
        public int _sale_num { set; get; }
        public int _amount { set; get; }

        public int CompareTo(object obj)//实现接口
        {
            MedicineSaleDataDTO temp = (MedicineSaleDataDTO)obj;
            return temp._sale_num - this._sale_num;
        }

    }
}
