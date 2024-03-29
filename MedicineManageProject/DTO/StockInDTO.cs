﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DTO
{
    public class StockInDTO
    {
        public String _medicine_id { set; get; }

        public String _batch_id { get; set; }

        public DateTime _in_time { get; set; }

        public int _in_num { get; set; }

        public Decimal _sale_price { set; get; }

        public DateTime _production_date { set; get; }

        public DateTime _validity_date { set; get; }

    }
}
