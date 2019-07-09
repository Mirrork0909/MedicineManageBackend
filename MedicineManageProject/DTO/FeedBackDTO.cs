using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class FeedbackDTO
    {
        public String _customer_id { get; set; }
        public String _suggest_content { get; set; }
        public String _customer_name { get; set; }
        public DateTime _time { get; set; }
    }
}
