using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineManageProject.DTO;
using MedicineManageProject.Model;

namespace MedicineManageProject.DB.Services
{
    public class MedicineManager:DBContext
    {
        public bool insertNewMedicine(MedicineDTO medicine)
        {
            try
            {
                Db.Ado.BeginTran();

                MEDICINE_INFORMATION medicineInformation = new MEDICINE_INFORMATION()
                {
                    MEDICINE_ID = medicine._medicine_id,
                    MEDICINE_APPLICABILITY = medicine._medicine_applicability,
                    MEDICINE_CHARACTER = medicine._medicine_character,
                    MEDICINE_ATTENTION = medicine._medicine_attention,
                    MEDICINE_NAME = medicine._medicine_name,
                    MEDICINE_TYPE = medicine._medicine_type,
                    MEDICINE_INGREDIENTS = medicine._medicine_ingredients,
                    MEDICINE_USAGE = medicine._medicine_usage
                };

                MEDICINE_INSTANCE medicineInstance = new MEDICINE_INSTANCE()
                {
                    MEDICINE_ID = medicine._medicine_id,
                    BATCH_ID = medicine._batch_id,
                    PRODUCTION_DATE = medicine._production_date,
                    VALIDITY_DATE = medicine._validity_date,
                    SALE_PRICE = medicine._sale_price,
                    PURCHASE_PRICE = medicine._purchase_price,
                    SUPPLIER_ID = medicine._supplier_id,
                    MEDICINE_IMAGE = medicine._medicine_image
                };

                Db.Insertable(medicineInformation).ExecuteCommand();
                Db.Insertable(medicineInstance).ExecuteCommand();

                Db.Ado.CommitTran();
                return true;

            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
                return false;
            }
        }

        


    }
}
