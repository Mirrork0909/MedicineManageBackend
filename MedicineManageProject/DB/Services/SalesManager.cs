using MedicineManageProject.DTO;
using MedicineManageProject.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DB.Services
{
    public class SalesManager:DBContext
    {
        public List<SaleInformationDTO> getSaleRecords()
        {
            var temp = Db.Queryable<SALE_RECORD>().OrderBy(it => it.SALE_DATE, OrderByType.Desc);
            List<SALE_RECORD> salesList = temp.Take(10).ToList();
            List<SaleInformationDTO> saleInformationDTO = new List<SaleInformationDTO>();
            foreach(SALE_RECORD sale in salesList)
            {
                SaleInformationDTO tempSale = new SaleInformationDTO();
                tempSale._customer_id = sale.CUSTOMER_ID;
                tempSale._sale_date = sale.SALE_DATE;
                tempSale._sale_id = sale.SALE_ID;
                tempSale._sale_price = sale.SALE_PRICE;
                tempSale._staff_id = sale.STAFF_ID;
                saleInformationDTO.Add(tempSale);
            }
            return saleInformationDTO;
        }
        public List<SalesDataDTO> getSalesAmountByMonth()
        {
            //).ToShortDateString().ToString()
            // var temp = Db.SqlQueryable<dynamic>("select to_char(SALE_DATE,'yyyymm')as time ,sum(SALE_PRICE) as amount  from SALE_RECORD group by to_char(SALE_DATE,'yyyymm')").ToList();
            var temp = Db.Queryable<SALE_RECORD>().GroupBy(it => SqlFunc.DateValue(it.SALE_DATE, DateType.Year)).GroupBy(it=>SqlFunc.DateValue(it.SALE_DATE, DateType.Month));
            temp.Select(it => new {time = SqlFunc.DateValue(it.SALE_DATE, DateType.Year) ,amount = SqlFunc.AggregateSum(it.SALE_PRICE) }).ToList(); 
            List<SalesDataDTO> salesDatas = new List<SalesDataDTO>();
          
           
            return salesDatas;
        }
    }
}
