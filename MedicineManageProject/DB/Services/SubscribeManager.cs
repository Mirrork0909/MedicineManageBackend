using MedicineManageProject.DTO;
using MedicineManageProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DB.Services
{
    public class SubscribeManager:DBContext
    {
        public bool insertSubsribe(SubscribeDTO s)
        {
            try
            {
                SUBSCRIBE temp = new SUBSCRIBE();
                temp.CUSTOMER_ID = s._customer_id;
                temp.MEDICINE_ID = s._medicine_id;
                Db.Insertable<SUBSCRIBE>(temp).ExecuteCommand();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public List<SubscribeDTO> getSubscribeDTOs(String id)
        {
            try
            {
                List<SubscribeDTO> subscribeDTOs = Db.Queryable<SUBSCRIBE>().Where((s) => s.CUSTOMER_ID == id).Select((s) => new SubscribeDTO
                {
                    _customer_id = id,
                    _medicine_id=s.MEDICINE_ID,
                    
                }).ToList();
                return subscribeDTOs;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public bool deleteSubscribe(SubscribeDTO s)
        {
            try
            {
                Db.Deleteable<SUBSCRIBE>().Where(new SUBSCRIBE() { CUSTOMER_ID = s._customer_id, MEDICINE_ID = s._medicine_id }).ExecuteCommand();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            
        }
    }
}
