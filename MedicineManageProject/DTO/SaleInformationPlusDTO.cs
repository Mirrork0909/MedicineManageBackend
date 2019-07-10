using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class SaleInformationPlusDTO:IComparable
    {
        public decimal _sale_id { get; set; }

        public string _customer_id { get; set; }

        public string _staff_id { get; set; }

        public DateTime _sale_date { get; set; }

        public decimal _sale_price { get; set; }

        public bool _is_return { get; set; }

        public int CompareTo(object obj)//实现接口
        {
            SaleInformationPlusDTO temp = (SaleInformationPlusDTO)obj;
            return temp._sale_date.CompareTo(this._sale_date);
        }
    }
}
