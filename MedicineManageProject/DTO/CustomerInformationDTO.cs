﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class CustomerInformationDTO
    {
        public String _name { get; set; }
        public String _id { get; set; }
        public String _phone { get; set; }
        public String _password { get; set; }
        public int _bouns_point { get; set; }
        public DateTime _sign_date { get; set; }
    }
}
