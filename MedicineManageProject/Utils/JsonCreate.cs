using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Utils
{
    public class JsonCreate
    {
        public String message { get; set; }
        public object data { get; set; }


        public static JsonCreate newInstance(String msg,object obj)
        {
            return new JsonCreate
            {
                message = msg,
                data = obj
            };
        }
    }

   

}
