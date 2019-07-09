using MedicineManageProject.DTO;
using MedicineManageProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Utils
{
    public class UserMergeUtil
    {
        public static JsonCreate addDataToResult(List<STAFF> staffs, List<CUSTOMER> customers)
        {
            JsonCreate resultJson = new JsonCreate();
            UserInformationListDTO userInformationListDTOs = new UserInformationListDTO();
            List<UserInformationDTO> userList = new List<UserInformationDTO>();
            if (staffs != null && staffs.Count > 0)
            {
                userList.AddRange(addIdentity(staffs, UserInformationDTO._STAFF));
            }
            if (customers != null && customers.Count > 0)
            {
                userList.AddRange(addIdentity(customers, UserInformationDTO._CUSTOMER));
            }
            userInformationListDTOs._count = userList.Count;
            userInformationListDTOs._userInformationDTOs = userList;
            resultJson.message = userInformationListDTOs._count == 0 ? ConstMessage.NOT_FOUND : ConstMessage.GET_SUCCESS;
            resultJson.data = userInformationListDTOs;
            return resultJson;
        }
        public static List<UserInformationDTO> addIdentity(List<CUSTOMER> users, String identity)
        {
            List<UserInformationDTO> userList = new List<UserInformationDTO>();
            foreach (CUSTOMER user in users)
            {
                UserInformationDTO userInformationDTO = new UserInformationDTO();
                userInformationDTO._id = user.CUSTOMER_ID;
                userInformationDTO._name = user.NAME;
                userInformationDTO._identity = identity;
                userInformationDTO._phone = user.PHONE;
                userInformationDTO._sign_date = user.SIGN_DATE;
                userList.Add(userInformationDTO);
            }
            return userList;
        }
        public static List<UserInformationDTO> addIdentity(List<STAFF> users, String identity)
        {
            List<UserInformationDTO> userList = new List<UserInformationDTO>();
            foreach (STAFF user in users)
            {
                UserInformationDTO userInformationDTO = new UserInformationDTO();
                userInformationDTO._id = user.STAFF_ID;
                userInformationDTO._name = user.NAME;
                userInformationDTO._identity = identity;
                userInformationDTO._phone = user.PHONE;
                userInformationDTO._sign_date = user.SIGN_DATE;
                userList.Add(userInformationDTO);
            }
            return userList;
        }
    }
}
