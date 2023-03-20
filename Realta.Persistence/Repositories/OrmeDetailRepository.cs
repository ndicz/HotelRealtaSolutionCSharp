using Microsoft.VisualBasic;
using Realta.Domain.Dto;
using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class OrmeDetailRepository : RepositoryBase<OrmeDetail>, IOrmeDetailRepository
    {
        public OrmeDetailRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(NewOrderMenusDto ormeDetail)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"UPDATE Resto.order_menus
                                SET orme_status = 'Ordered',
                                orme_pay_type = @OrmePayType,
                                orme_cardnumber = @OrmeCardnumber,
                                orme_is_paid = @OrmeIsPaid
                                WHERE orme_id = @OrmeId;",

                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                        new SqlCommandParameterModel() {
                        ParameterName = "@OrmePayType",
                        DataType = DbType.String,
                        Value = ormeDetail.OrmePayType
                    },
                        new SqlCommandParameterModel() {
                        ParameterName = "@OrmeCardnumber",
                        DataType = DbType.String,
                        Value = ormeDetail.OrmeCardnumber
                    },
                        new SqlCommandParameterModel() {
                        ParameterName = "@OrmeIsPaid",
                        DataType = DbType.String,
                        Value = ormeDetail.OrmeIsPaid
                    },
                           new SqlCommandParameterModel() {
                        ParameterName = "@OrmeId",
                        DataType = DbType.Int32,
                        Value = ormeDetail.OrmeId
                    }

            }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();

        }

        public IEnumerable<OrmeDetail> FindAllOrmeDetail()
        {
            IEnumerator<OrmeDetail> dataSet = FindAll<OrmeDetail>("SELECT  * From Resto.order_menu_detail");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public async Task<IEnumerable<OrmeDetail>> FindAllOrmeDetailAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Resto.order_menu_detail;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };
            IAsyncEnumerator<OrmeDetail> dataSet = FindAllAsync<OrmeDetail>(model);

            var item = new List<OrmeDetail>();


            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }


            return item;
        }

        public OrmeDetail FindOrmeDetailById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Resto.order_menu_detail where omde_id=@omde_id order by omde_id asc;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@omde_id",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<OrmeDetail>(model);

            OrmeDetail? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }


            return item;
        }

        public void Insert(NewOrderMenusDto orderMenusDto)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "Resto.create_order_menu_detail",

                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@omde_reme_id",
                        DataType = DbType.Int32,
                        Value = orderMenusDto.OmdeRemeId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_price",
                        DataType = DbType.Decimal,
                        Value = orderMenusDto.OrmePrice
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_qty",
                        DataType = DbType.Int16,
                        Value = orderMenusDto.OrmeQty
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@orme_discount",
                        DataType= DbType.Decimal,
                        Value = orderMenusDto.OrmeDiscount
                    },
                    new SqlCommandParameterModel() {
                    ParameterName = "@orme_pay_type",
                    DataType = DbType.String,
                    Value = orderMenusDto.OrmePayType
                    },
                    new SqlCommandParameterModel() {
                    ParameterName = "@orme_cardnumber",
                    DataType = DbType.String,
                    Value = orderMenusDto.OrmeCardnumber
                    },
                     new SqlCommandParameterModel() {
                    ParameterName = "@orme_is_paid",
                    DataType = DbType.String,
                    Value = orderMenusDto.OrmeIsPaid
                    },
                       new SqlCommandParameterModel() {
                    ParameterName = "@orme_user_id",
                    DataType = DbType.Int32,
                    Value = orderMenusDto.OrmeUserId
                    },
                         new SqlCommandParameterModel() {
                    ParameterName = "@orme_status",
                    DataType = DbType.String,
                    Value = orderMenusDto.OrmeStatus
                    }
                }

            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(OrmeDetail ormeDetail)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM Resto.order_menu_detail WHERE omde_id = @omde_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@omde_id",
                        DataType = DbType.Int32,
                        Value = ormeDetail.OmdeId
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<OrmeDetail> FindLastOrmeDetailId()
        {
            IEnumerator<OrmeDetail> dataset = FindAll<OrmeDetail>("SELECT * FROM Resto.order_menu_detail where omde_id =(SELECT IDENT_CURRENT('Resto.order_menu_detail'));");
            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }
        }
    }
}
