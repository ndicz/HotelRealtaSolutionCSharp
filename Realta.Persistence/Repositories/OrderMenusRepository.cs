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
                "orme_total_amount = @orme_total_amount" +
                "orme_pay_type = @orme_pay_type" +
                "orme_cardnumber = @orme_cardnumber" +
                "orme_is_paid = @orme_is_paid" +
                "orme_modified_date = @orme_modified_date" +
                "orme_user_id = @orme_user_id" +
                "WHERE orme_id = @orme_id;",

                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_order_number",
                        DataType = DbType.String,
                        Value = orderMenus.orme_order_number
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_order_date",
                        DataType = DbType.DateTime,
                        Value = orderMenus.orme_order_date
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_total_item",
                        DataType = DbType.Int16,
                        Value = orderMenus.orme_total_item
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_total_discount",
                        DataType = DbType.Decimal,
                        Value = orderMenus.orme_total_discount
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_total_amount",
                        DataType = DbType.Decimal,
                        Value = orderMenus.orme_total_amount
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_pay_type",
                        DataType = DbType.String,
                        Value = orderMenus.orme_pay_type
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_cardnumber",
                        DataType = DbType.String,
                        Value = orderMenus.orme_cardnumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_is_paid",
                        DataType = DbType.String,
                        Value = orderMenus.orme_is_paid
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_modified_date",
                        DataType = DbType.DateTime,
                        Value = orderMenus.orme_modified_date
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_user_id",
                        DataType = DbType.Int32,
                        Value = orderMenus.orme_user_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_id",
                        DataType = DbType.Int32,
                        Value = orderMenus.orme_id
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
            throw new NotImplementedException();
        }

        public void Remove(OrderMenus orderMenus)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderMenus> FindLastOrderMenusId()
        {
            throw new NotImplementedException();
        }

       
    }
}
