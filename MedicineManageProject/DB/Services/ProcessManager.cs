using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineManageProject.Model;
using MedicineManageProject.DTO;

namespace MedicineManageProject.DB.Services
{
    public class ProcessManager:DBContext
    {
        public bool processProblemMedicine(ProcessDTO processDTO)
        {
            try
            {
                Db.Ado.BeginTran();
                DateTime dateTime = DateTime.Now;

                var tempResult = Db.Queryable<MEDICINE_STOCK, STOCK_IN, CONTRACT>
                    ((ms, si, c) => ms.STOCK_ID == si.STOCK_ID && si.CONTRACT_ID == c.CONTRACT_ID).
                    Where((ms, si, c) => ms.BATCH_ID == processDTO._batch_id && ms.MEDICINE_ID == processDTO._medicine_id)
                    .Select((ms, si, c) => new
                    {
                        ms.STOCK_ID,
                        c.CONTRACT_ID,
                        ms.AMOUNT
                    }).Single();

                if(tempResult.AMOUNT < processDTO._num)
                {
                    Db.Ado.RollbackTran();
                    return false;
                }

                PROBLEM_MEDICINE_RETURN problemMedicineReturn = new PROBLEM_MEDICINE_RETURN
                {
                    STOCK_ID = tempResult.STOCK_ID,
                    CONTRACT_ID = tempResult.CONTRACT_ID,
                    STAFF_ID = processDTO._staff_id,
                    RETURN_NUMBER = processDTO._num,
                    RETURN_DATE = dateTime
                };

                Db.Insertable(problemMedicineReturn).ExecuteCommand();

                MEDICINE_STOCK medicine = new MEDICINE_STOCK
                {
                    STOCK_ID = tempResult.STOCK_ID,
                    MEDICINE_ID = processDTO._medicine_id,
                    BATCH_ID = processDTO._batch_id,
                    AMOUNT = tempResult.AMOUNT - processDTO._num
                };
                Db.Updateable(medicine).ExecuteCommand();

                Db.Ado.CommitTran();
                return true;


            }catch(Exception e)
            {
                Db.Ado.RollbackTran();
                return false;
            }
        }

        public bool processExpiredMedicine(ProcessDTO processDTO)
        {
            try
            {
                Db.Ado.BeginTran();
                DateTime dateTime = DateTime.Now;

                var tempResult = Db.Queryable<MEDICINE_STOCK>()
                    .Where(it => it.MEDICINE_ID == processDTO._medicine_id &&
                        it.BATCH_ID == processDTO._batch_id).Single();

                EXPIRED_MEDICINE_PROCESS expiredMedicine = new EXPIRED_MEDICINE_PROCESS
                {
                    STOCK_ID = tempResult.STOCK_ID,
                    STAFF_ID = processDTO._staff_id,
                    PROCESS_DATE = dateTime,
                    PROCESS_NUMBER = tempResult.AMOUNT
                };

                Db.Insertable(expiredMedicine).ExecuteCommand();

                tempResult.AMOUNT = 0;
                Db.Updateable(expiredMedicine).ExecuteCommand();



                Db.Ado.CommitTran();
                return true;

            }catch(Exception e)
            {
                Db.Ado.RollbackTran();
                return false;
            }
        }
    }
}
