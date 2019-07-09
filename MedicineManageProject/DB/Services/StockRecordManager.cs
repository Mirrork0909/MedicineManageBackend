using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
using MedicineManageProject.Utils;
using MedicineManageProject.DTO;
using MedicineManageProject.Model;

namespace MedicineManageProject.DB.Services
{
    public class StockRecordManager:DBContext
    {
        public List<StockRecordDTO> getInStcokRecords()
        {
            List<StockRecordDTO> result_purchase =
                Db.Queryable<STOCK_IN, MEDICINE_STOCK, MEDICINE_INFORMATION>
                ((si, ms, mi) => si.STOCK_ID == ms.STOCK_ID && ms.MEDICINE_ID == mi.MEDICINE_ID)
                .Select((si, ms, mi) => new StockRecordDTO
                {
                    _medicine_id = ms.MEDICINE_ID,
                    _batch_id = ms.BATCH_ID,
                    _medicine_name = mi.MEDICINE_NAME,
                    _amount = si.IN_NUM,
                    _time = si.IN_TIME,
                    _reason = StockMessage.STOCK_IN
                }).ToList();
            List<StockRecordDTO> result_back =
                Db.Queryable<RETURN_RECORD, SALE_RECORD_ITEM, MEDICINE_INFORMATION>
                ((rr, sri, mi) => rr.SALE_ID == sri.SALE_ID && rr.ORDER_ID == sri.ORDER_ID &&
                    sri.MEDICINE_ID == mi.MEDICINE_ID)
                .Select((rr, sri, mi) => new StockRecordDTO
                {
                    _medicine_id = sri.MEDICINE_ID,
                    _batch_id = sri.BATCH_ID,
                    _medicine_name = mi.MEDICINE_NAME,
                    _amount = rr.BACK_NUM,
                    _time = rr.BACK_DATE,
                    _reason = StockMessage.BACK_IN
                }).ToList();
            List<StockRecordDTO> result = result_purchase.Union(result_back).ToList();
            result = result.OrderBy(it => it._time).ToList();
            return result;
        }

        public List<StockRecordDTO> getOutStcokRecords()
        {
            List<StockRecordDTO> result_problem =
                Db.Queryable<PROBLEM_MEDICINE_RETURN, MEDICINE_STOCK, MEDICINE_INFORMATION>
                ((pmr, ms, mi) => pmr.STOCK_ID == ms.STOCK_ID && ms.MEDICINE_ID == mi.MEDICINE_ID)
                .Select((pmr, ms, mi) => new StockRecordDTO
                {
                    
                    _medicine_id = ms.MEDICINE_ID,
                    _batch_id = ms.BATCH_ID,
                    _medicine_name = mi.MEDICINE_NAME,
                    _amount = pmr.RETURN_NUMBER,
                    _time = pmr.RETURN_DATE,
                    _reason = StockMessage.PROBLEM_MEDICINE
                }).ToList();

            List<StockRecordDTO> result_expired =
                Db.Queryable<EXPIRED_MEDICINE_PROCESS, MEDICINE_STOCK, MEDICINE_INFORMATION>
                ((emp, ms, mi) => emp.STOCK_ID == ms.STOCK_ID && ms.MEDICINE_ID == mi.MEDICINE_ID)
                .Select((emp, ms, mi) => new StockRecordDTO
                {
                    _medicine_id = ms.MEDICINE_ID,
                    _batch_id = ms.BATCH_ID,
                    _medicine_name = mi.MEDICINE_NAME,
                    _amount = emp.PROCESS_NUMBER,
                    _time = emp.PROCESS_DATE,
                    _reason = StockMessage.EXPIRED_MEDICINE
                }).ToList();

            List<StockRecordDTO> result_sale =
                Db.Queryable<STOCK_OUT, MEDICINE_STOCK, MEDICINE_INFORMATION>
                ((so, ms, mi) => so.STOCK_ID == ms.STOCK_ID && ms.MEDICINE_ID == mi.MEDICINE_ID)
                .Select((so, ms, mi) => new StockRecordDTO
                {
                    _medicine_id = ms.MEDICINE_ID,
                    _batch_id = ms.BATCH_ID,
                    _medicine_name = mi.MEDICINE_NAME,
                    _amount = so.OUT_NUM,
                    _time = so.OUT_TIME,
                    _reason = StockMessage.SOLED_OUT
                }).ToList();

            List<StockRecordDTO> result = result_problem.Union(result_expired).Union(result_sale).ToList();
            result = result.OrderBy(it => it._time).ToList();
            return result;
        }
    }
}
