using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class UserInformationDTO
    {
        public static String _CUSTOMER = "CUSTOMER";
        public static String _STAFF = "STAFF";
        public String  _name { get; set; }
        public String _id { get; set; }
        public String _identity { get; set; }
        public String _phone { get; set; }
        public DateTime _sign_date { get; set; }

    }
}
