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

        public void Edit(OrmeDetail ormeDetail)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE Resto.order_menu_detail set  orme_price = @orme_price," +
                "orme_qty = @orme_qty," +
                "orme_subtotal = @orme_subtotal," +
                "orme_discount = @orme_discount," +
                "omde_orme_id = @omde_orme_id," +
                "omde_reme_id =" +
                " @omde_reme_id WHERE omde_id = @omde_id;",

                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                        new SqlCommandParameterModel() {
                        ParameterName = "@omde_id",
                        DataType = DbType.Int32,
                        Value = ormeDetail.omde_id
                    },
                        new SqlCommandParameterModel() {
                        ParameterName = "@orme_price",
                        DataType = DbType.Decimal,
                        Value = ormeDetail.orme_price
                    },
                        new SqlCommandParameterModel() {
                        ParameterName = "@orme_qty",
                        DataType = DbType.Int16,
                        Value = ormeDetail.orme_qty
                    },
                        new SqlCommandParameterModel() {
                        ParameterName = "@orme_subtotal",
                        DataType = DbType.Decimal,
                        Value = ormeDetail.orme_subtotal
                    },
                        new SqlCommandParameterModel() {
                        ParameterName = "@orme_discount",
                        DataType = DbType.Decimal,
                        Value = ormeDetail.orme_discount
                    },
                        new SqlCommandParameterModel() {
                        ParameterName = "@omde_orme_id",
                        DataType = DbType.Int32,
                        Value = ormeDetail.omde_orme_id
                    },
                                new SqlCommandParameterModel() {
                        ParameterName = "@omde_reme_id",
                        DataType = DbType.Int32,
                        Value = ormeDetail.omde_reme_id
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

        public void Insert(OrmeDetail ormeDetail)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO Resto.order_menu_detail (orme_price, orme_qty,orme_subtotal,orme_discount,omde_orme_id,omde_reme_id) " +
              "VALUES(@orme_price,@orme_qty,@orme_subtotal,@orme_discount,@omde_orme_id,@omde_reme_id)",

                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_price",
                        DataType = DbType.Int32,
                        Value = ormeDetail.orme_price
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_qty",
                        DataType = DbType.String,
                        Value = ormeDetail.orme_qty
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@orme_subtotal",
                        DataType = DbType.String,
                        Value = ormeDetail.orme_subtotal
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@orme_discount",
                        DataType= DbType.Decimal,
                        Value = ormeDetail.orme_discount
                    },
                    new SqlCommandParameterModel() {
                    ParameterName = "@omde_orme_id",
                    DataType = DbType.String,
                    Value = ormeDetail.omde_orme_id
                    },
                    new SqlCommandParameterModel() {
                    ParameterName = "@omde_reme_id",
                    DataType = DbType.Int32,
                    Value = ormeDetail.omde_reme_id
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
                        Value = ormeDetail.omde_id
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
