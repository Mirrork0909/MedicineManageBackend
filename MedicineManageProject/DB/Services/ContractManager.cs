using MedicineManageProject.DTO;
using MedicineManageProject.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DB.Services
{
    public class ContractManager:DBContext
    {
        public List<ContractDTO> getAllContract()
        {
            var allContract = Db.Queryable<CONTRACT>().ToList();
            List<ContractDTO> contractDTOs = new List<ContractDTO>();
            if (allContract != null)
            {
                foreach (CONTRACT contract in allContract)
                {
                    ContractDTO temp = new ContractDTO();
                    temp._contract_id = contract.CONTRACT_ID;
                    temp._contract_status = contract.CONTRACT_STATUS;
                    temp._delivery_date = contract.DELIVERY_DATE;
                    temp._sign_date = contract.SIGN_DATE;
                    temp._supplier_id = contract.SUPPLIER_ID;
                    temp._staff_id = contract.STAFF_ID;
                    contractDTOs.Add(temp);
                }
                return contractDTOs;
            }
            else
            {
                return null;
            }
        }

        public ContractDTO getAllContractItem(int id)
        {
            ContractDTO contractDTO = Db.Queryable<CONTRACT,SUPPLIER>((c,s) => 
            s.SUPPLIER_ID==c.SUPPLIER_ID).Where((c)=>c.CONTRACT_ID==id)           
            .Select((c,s) => new ContractDTO{ _contract_id = c.CONTRACT_ID,_supplier_id = c.SUPPLIER_ID,_name=s.NAME,_phone=s.PHONE,_credit_level=s.CREDIT_LEVEL
            }).Single();
            if (contractDTO != null)
            {
                contractDTO._contract_items = Db.Queryable<CONTRACT_ITEM>().Where((ci) => ci.CONTRACT_ID == id)
                .Select((ci) => new ContractItemDTO
                {
                    _medicine_id = ci.MEDICINE_ID,
                    _medicine_money = ci.MEDICINE_MONEY,
                    _medicine_status=ci.MEDICINE_STATUS,
                    _medicine_amount=ci.MEDICINE_AMOUNT

                }
            ).ToList();
            return contractDTO;
            }
            return null;
            
        }

        public ContractDTO insertContract(ContractDTO contractDTO)
        {
            try
            {

                Db.Ado.BeginTran();
                CONTRACT contract = new CONTRACT()
                {
                    DELIVERY_DATE = contractDTO._delivery_date,
                    SUPPLIER_ID = contractDTO._supplier_id,
                    CONTRACT_STATUS = 0,
                    STAFF_ID = contractDTO._staff_id,
                    //SIGN_DATE = contractDTO._sign_date
                };
                Db.Insertable(contract).IgnoreColumns(it => new {it.CONTRACT_ID }).ExecuteCommand();

                var cid = Db.Ado.SqlQuery<int>("select ISEQ$$_75593.currval from dual");
                var id = cid[0];
                if (contractDTO._contract_items != null)
                {
                    foreach (ContractItemDTO temp in contractDTO._contract_items)
                    {
                        CONTRACT_ITEM contractItem = new CONTRACT_ITEM()
                        {
                            CONTRACT_ID = id,
                            MEDICINE_ID = temp._medicine_id,
                            MEDICINE_MONEY = temp._medicine_money,
                            MEDICINE_AMOUNT = temp._medicine_amount,
                            MEDICINE_STATUS = 0,
                        };
                        Db.Insertable(contractItem).ExecuteCommand();
                    }
                }
                Db.Ado.CommitTran();
                contractDTO._contract_id = id;
                return contractDTO;
                
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="completeContractDTO"></param>
        public bool completeOneContract(CompleteContractDTO completeContractDTO)
        {
            try
            {

                Db.Ado.BeginTran();
                CONTRACT contract = Db.Queryable<CONTRACT>().InSingle(completeContractDTO._contract_id);
                contract.CONTRACT_STATUS = 2;

                List<CONTRACT_ITEM> list = Db.Queryable<CONTRACT_ITEM>().Where((it) => it.CONTRACT_ID == completeContractDTO._contract_id).ToList();
                if (list != null)
                {
                    for (int i = 0; i < completeContractDTO.stockInDTOs.Count; i++)
                    {
                        list[i].MEDICINE_STATUS = 2;
                        Db.Updateable(list[i]).ExecuteCommand();




                        var isInStock = Db.Queryable<MEDICINE_STOCK>().Where((ms) => ms.MEDICINE_ID == completeContractDTO.stockInDTOs[i]._medicine_id
                        && completeContractDTO.stockInDTOs[i]._batch_id == ms.BATCH_ID).First();
                        if (isInStock == null)
                        {
                            return false;
                        }
                        else
                        {
                            isInStock.AMOUNT += completeContractDTO.stockInDTOs[i]._in_num;
                            Db.Updateable(isInStock).ExecuteCommand();
                        }

                        STOCK_IN stock_in = new STOCK_IN();
                        stock_in.STOCK_ID = isInStock.STOCK_ID;
                        stock_in.CONTRACT_ID = completeContractDTO._contract_id;
                        stock_in.IN_NUM = completeContractDTO.stockInDTOs[i]._in_num;
                        stock_in.IN_TIME = completeContractDTO.stockInDTOs[i]._in_time;
                        Db.Insertable(stock_in).ExecuteCommand();
                    }
                }
                Db.Ado.CommitTran();
                return true;
            }
            catch(Exception e)
            {
                Db.Ado.RollbackTran();
                return false;
            }
            
            
            //stock_in.IN_NUM=contract.
        }

        public List<CostByMonthDTO> getCostByMonth()
        {
            //得到包含年，月，钱的列表
            List<CostByMonthDTO> list = Db.Queryable<CONTRACT, CONTRACT_ITEM>((c, ci) => c.CONTRACT_ID == ci.CONTRACT_ID).
                Select((c, ci) => new CostByMonthDTO
                {
                    _year = c.SIGN_DATE.Year,
                    _month = c.SIGN_DATE.Month,
                    _cost = ci.MEDICINE_MONEY
                }).ToList();
            //分组
            for (int i = 0; i < list.Count; i++)
            {
                for(int j = i+1; j < list.Count; j++)
                {
                    if (list[i]._year == list[j]._year && list[i]._month == list[j]._month)
                    {
                        list[i]._cost += list[j]._cost;
                        list.RemoveAt(j);
                        j--;
                    }
                }
            }
            list.Sort(new YMSortCompare());
            return list;


        }
    }
}
