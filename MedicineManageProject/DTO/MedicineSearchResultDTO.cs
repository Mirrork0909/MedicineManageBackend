using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class MedicineSearchResultDTO
    {
        public String _medicine_id { set; get; }
        public String _medicine_name { set; get; }
        public String _medicine_type { set; get; }
        public String _medicine_ingredients { set; get; }
        public String _medicine_character { set; get; }
        public String _medicine_applicability { set; get; }
        public String _medicine_usage { set; get; }
        public String _medicine_attention { set; get; }
        public Decimal _supplier_id { get; set; }
        public String _medicine_image { set; get; }
        public decimal _max_purchase_price { set; get; }
        public decimal _min_purchase_price { set; get; }
    }
}
