using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class MedicineDTO
    {
        public String _medicine_id { set; get; }
        public String _medicine_name { set; get; }
        public String _medicine_type { set; get; }
        public String _medicine_ingredients { set; get; }
        public String _medicine_character { set; get; }
        public String _medicine_applicability { set; get; }
        public String _medicine_usage { set; get; }
        public String _medicine_attention { set; get; }
        public String _medicine_image { set; get; }
        public String _batch_id { get; set; }
        public Decimal _purchase_price { get; set; }
        public Decimal _sale_price { get; set; }
        public Decimal _discount_price { get; set; }
        public Decimal _supplier_id { get; set; }
        public int _medicine_amount { get; set; }
        public DateTime _production_date { get; set; }
        public DateTime _validity_date { get; set; }
    }
}
