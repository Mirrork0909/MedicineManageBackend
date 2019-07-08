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
        public void insertNewMedicine(MedicineInformation medicineInformation)
        {
            MEDICINE_INFORMATION _INFORMATION = new MEDICINE_INFORMATION();
            _INFORMATION.MEDICINE_ID = medicineInformation.MEDICINE_ID;
            _INFORMATION.MEDICINE_NAME = medicineInformation.MEDICINE_NAME;
            _INFORMATION.MEDICINE_INGREDIENTS = medicineInformation.MEDICINE_INGREDIENT;
            _INFORMATION.MEDICINE_ATTENTION = medicineInformation.MEDICINE_ATTENTION;
            _INFORMATION.MEDICINE_TYPE = medicineInformation.MEDICINE_TYPE;
            _INFORMATION.MEDICINE_USAGE = medicineInformation.MEDICINE_USAGE;
            _INFORMATION.MEDICINE_IMAGE = medicineInformation.MEDICINE_IMAGE;
            _INFORMATION.MEDICINE_CHARACTER = medicineInformation.MEDICINE_CHARACTER;
            _INFORMATION.MEDICINE_APPLICABILITY = medicineInformation.MEDICINE_APPLICABILITY;
            medicine_information_DB.Insert(_INFORMATION);
        }




    }
}
