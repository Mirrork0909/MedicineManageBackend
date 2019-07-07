using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
using MedicineManageProject.Model;

namespace MedicineManageProject.DB
{
    public class DBContext
    {
        public SqlSugarClient Db;
        public DBContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Data Source=47.102.158.185/orcl;User ID = C##YIYAODB; Password = YIyao123;",
                DbType = DbType.Oracle,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" + Db.Utilities.SerializeObject
                    (pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
        }

        public SimpleClient<MEDICINE_INFORMATION> medicine_information_DB { get { return new SimpleClient<MEDICINE_INFORMATION>(Db); } }
        public SimpleClient<MEDICINE_INSTANCE> medicine_instance_DB { get { return new SimpleClient<MEDICINE_INSTANCE>(Db); } }
        public SimpleClient<MEDICINE_STOCK> medicine_stock_DB { get { return new SimpleClient<MEDICINE_STOCK>(Db); } }
        public SimpleClient<DISCOUNT> discount_DB { get { return new SimpleClient<DISCOUNT>(Db); } }
        public SimpleClient<SET_DISCOUNT> setDiscount_DB { get { return new SimpleClient<SET_DISCOUNT>(Db); } }
        public SimpleClient<STAFF> staff_DB { get { return new SimpleClient<STAFF>(Db); } }
        public SimpleClient<EXPIRED_MEDICINE_PROCESS> expired_medicine_process_DB { get { return new SimpleClient<EXPIRED_MEDICINE_PROCESS>(Db); } }
    }
}
