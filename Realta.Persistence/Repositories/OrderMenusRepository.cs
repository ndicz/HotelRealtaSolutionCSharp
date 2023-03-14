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
    internal class OrdermenusRepository : RepositoryBase<OrderMenus>, IOrderMenusRepository
    {
        public OrdermenusRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(OrderMenus orderMenus)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE Resto.order_menus set " +
                "orme_order_number = @orme_order_number," +
                "orme_order_date = @orme_order_date," +
                "orme_total_item = @orme_total_item," +
                "orme_total_discount = @orme_total_discount," +
                "orme_total_amount = @orme_total_amount," +
                "orme_pay_type = @orme_pay_type," +
                "orme_cardnumber = @orme_cardnumber," +
                "orme_is_paid = @orme_is_paid," +
                "orme_modified_date = @orme_modified_date," +
                "orme_user_id = @orme_user_id " +
                "WHERE orme_id = @orme_id;",

                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_order_number",
                        DataType = DbType.String,
                        Value = orderMenus.OrmeOrderNumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_order_date",
                        DataType = DbType.DateTime,
                        Value = orderMenus.OrmeOrderDate
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_total_item",
                        DataType = DbType.Int16,
                        Value = orderMenus.OrmeTotalItem
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_total_discount",
                        DataType = DbType.Decimal,
                        Value = orderMenus.OrmeTotalDiscount
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_total_amount",
                        DataType = DbType.Decimal,
                        Value = orderMenus.OrmeTotalAmount
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_pay_type",
                        DataType = DbType.String,
                        Value = orderMenus.OrmePayType
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_cardnumber",
                        DataType = DbType.String,
                        Value = orderMenus.ormeCardnumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_is_paid",
                        DataType = DbType.String,
                        Value = orderMenus.OrmeIsPaid
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_modified_date",
                        DataType = DbType.DateTime,
                        Value = orderMenus.OrmeModifiedDate
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_user_id",
                        DataType = DbType.Int32,
                        Value = orderMenus.OrmeUserId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_id",
                        DataType = DbType.Int32,
                        Value = orderMenus.OrmeId
                    }
  }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<OrderMenus> FindAllOrderMenus()
        {
            IEnumerator<OrderMenus> dataSet = FindAll<OrderMenus>("SELECT  * From Resto.order_menus");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public async Task<IEnumerable<OrderMenus>> FindAllOrderMenusAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Resto.order_menus;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };
            IAsyncEnumerator<OrderMenus> dataSet = FindAllAsync<OrderMenus>(model);

            var item = new List<OrderMenus>();


            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }


            return item;
        }

        public OrderMenus FindOrderMenusById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Resto.order_menus where orme_id=@orme_id order by orme_id asc;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_id",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<OrderMenus>(model);

            OrderMenus? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }


            return item;
        }

        public void Insert(OrderMenus orderMenus)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO Resto.order_menus (orme_order_date, orme_total_item, orme_total_discount, orme_total_amount, orme_pay_type, orme_cardnumber, orme_is_paid, orme_modified_date, orme_user_id)" +
                "VALUES(@orme_order_date, @orme_total_item, @orme_total_discount, @orme_total_amount, @orme_pay_type, @orme_cardnumber, @orme_is_paid, @orme_modified_date, @orme_user_id)",

                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_order_date",
                        DataType = DbType.DateTime,
                        Value = orderMenus.OrmeOrderDate
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_total_item",
                        DataType = DbType.Int16,
                        Value = orderMenus.OrmeTotalItem
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_total_discount",
                        DataType = DbType.Decimal,
                        Value = orderMenus.OrmeTotalDiscount
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_total_amount",
                        DataType = DbType.Decimal,
                        Value = orderMenus.OrmeTotalAmount
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_pay_type",
                        DataType = DbType.String,
                        Value = orderMenus.OrmePayType
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_cardnumber",
                        DataType = DbType.String,
                        Value = orderMenus.ormeCardnumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_is_paid",
                        DataType = DbType.String,
                        Value = orderMenus.OrmeIsPaid
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_modified_date",
                        DataType = DbType.DateTime,
                        Value = orderMenus.OrmeModifiedDate
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_user_id",
                        DataType = DbType.Int32,
                        Value = orderMenus.OrmeUserId
                       }

                }

            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public void Remove(OrderMenus orderMenus)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM Resto.order_menus WHERE orme_id = @orme_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_id",
                        DataType = DbType.Int32,
                        Value = orderMenus.OrmeId
                }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();// membersihkan resource dan close connection
        }

        public IEnumerable<OrderMenus> FindLastOrderMenusId()
        {
            IEnumerator<OrderMenus> dataset = FindAll<OrderMenus>("SELECT * FROM Resto.order_menus where orme_id =(SELECT IDENT_CURRENT('Resto.order_menus'));");
            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }
        }

        public OrderMenusNestedMenusDetail GetOrmeNestedMenuDetail(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "Resto.GetOrderMenusWithDetailsById",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {

                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@orme_id",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<OrderMenusJoinMenusDetail>(model);
            var listData = new List<OrderMenusJoinMenusDetail>();
            while (dataSet.MoveNext())
            {
                listData.Add(dataSet.Current);
            }
            var orderMenus = listData.Select(x => new { x.orme_id, x.orme_order_number, x.orme_total_amount, x.orme_total_discount, x.reme_name }).FirstOrDefault();

            var orderDetail = listData.Select(x => new OrmeDetail
            {
                OmdeId = x.omde_id,
                OrmePrice = x.orme_price,
                OrmeQty = x.orme_qty,
                OrmeSubtotal = x.orme_subtotal,
                OmdeOrmeId = x.omde_orme_id,
                OmdeRemeId = x.omde_reme_id,
                RemeName = x.reme_name
            });

            var nestedJson = new OrderMenusNestedMenusDetail
            {
                orme_id = orderMenus.orme_id,
                orme_order_number = orderMenus.orme_order_number,
                orme_total_amount = orderMenus.orme_total_amount,
                orme_total_discount = orderMenus.orme_total_discount,
                MenuDetail = orderDetail.ToList()
            };

            return nestedJson;

        }
    }
}
