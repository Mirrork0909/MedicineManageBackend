using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
using MedicineManageProject.Model;
using MedicineManageProject.DTO;

namespace MedicineManageProject.DB.Services
{
    public class FeedbackManager : DBContext
    {
        public bool insertOneFeedBack(FeedbackDTO feedBackDTO)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                FEEDBACK feedback = new FEEDBACK
                {
                    CUSTOMER_ID = feedBackDTO._customer_id,
                    SUGGEST_CONTENT = feedBackDTO._suggest_content,
                    SUGGEST_DATE = dateTime
                };
                Db.Insertable(feedback).IgnoreColumns(it => new { it.SUGGEST_ID }).ExecuteCommand();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<FeedbackDTO> getAllFeedBack()
        {
            try
            {
                List<FeedbackDTO> fbList = Db.Queryable<FEEDBACK, CUSTOMER>((fb, c) => fb.CUSTOMER_ID == c.CUSTOMER_ID)
                    .OrderBy((fb,c) => fb.SUGGEST_DATE, OrderByType.Desc)
                    .Select((fb, c) => new FeedbackDTO
                    {
                        _customer_id = fb.CUSTOMER_ID,
                        _customer_name = c.NAME,
                        _suggest_content = fb.SUGGEST_CONTENT,
                        _time = fb.SUGGEST_DATE
                    }).ToList();
                return fbList;
            }
            catch (Exception e)
            {
                return new List<FeedbackDTO>();
            }

        }
    }
}
