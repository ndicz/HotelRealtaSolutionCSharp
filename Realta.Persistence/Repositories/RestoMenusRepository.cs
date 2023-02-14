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
    internal class RestoMenusRepository : RepositoryBase<RestoMenus>, IRestoMenusRepository
    {
        public RestoMenusRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(RestoMenus restoMenus)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE Resto.resto_menus set  reme_name = @reme_name," +
                "reme_description = @reme_desc," +
                "reme_price = @reme_price," +
                "reme_status = @reme_status," +
                "reme_modified_date =" +
                " @reme_mod WHERE reme_id = @reme_id;",

                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@reme_name",
                        DataType = DbType.String,
                        Value = restoMenus.reme_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@reme_desc",
                        DataType = DbType.String,
                        Value = restoMenus.reme_description
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@reme_price",
                        DataType= DbType.Decimal,
                        Value = restoMenus.reme_price
                    },
                        new SqlCommandParameterModel() {
                        ParameterName = "@reme_status",
                        DataType = DbType.String,
                        Value = restoMenus.reme_status
                    },
                        new SqlCommandParameterModel() {
                        ParameterName = "@reme_mod",
                        DataType = DbType.DateTime,
                        Value = restoMenus.reme_modified_date
                    },

                        new SqlCommandParameterModel() {
                        ParameterName = "@reme_id",
                        DataType = DbType.Int32,
                        Value = restoMenus.reme_id
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<RestoMenus> FindAllRestoMenus()
        {
            IEnumerator<RestoMenus> dataSet = FindAll<RestoMenus>("SELECT  * From Resto.resto_menus");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public async Task<IEnumerable<RestoMenus>> FindAllRestoAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Resto.resto_menus;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };
            IAsyncEnumerator<RestoMenus> dataSet = FindAllAsync<RestoMenus>(model);

            var item = new List<RestoMenus>();


            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }


            return item;
        }

        public RestoMenus FindRestoById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Resto.resto_menus where reme_id=@reme_id order by reme_id asc;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@reme_id",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<RestoMenus>(model);

            RestoMenus? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }


            return item;
        }

        public void Insert(RestoMenus restoMenus)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO Resto.resto_menus (reme_faci_id,reme_name,reme_description,reme_price,reme_status,reme_modified_date) " +
               "VALUES(@reme_faci_id,@reme_name,@reme_desc,@reme_price,@reme_status,@reme_mod)",

                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@reme_faci_id",
                        DataType = DbType.Int32,
                        Value = restoMenus.reme_faci_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@reme_name",
                        DataType = DbType.String,
                        Value = restoMenus.reme_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@reme_desc",
                        DataType = DbType.String,
                        Value = restoMenus.reme_description
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@reme_price",
                        DataType= DbType.Decimal,
                        Value = restoMenus.reme_price
                    },
                    new SqlCommandParameterModel() {
                    ParameterName = "@reme_status",
                    DataType = DbType.String,
                    Value = restoMenus.reme_name
                    },
                    new SqlCommandParameterModel() {
                    ParameterName = "@reme_mod",
                    DataType = DbType.DateTime,
                    Value = restoMenus.reme_modified_date
                    }

                }

            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();

        }


        public void Remove(RestoMenus restoMenus)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM Resto.resto_menus WHERE reme_id = @reme_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@reme_id",
                        DataType = DbType.Int32,
                        Value = restoMenus.reme_id
                }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }



        public IEnumerable<RestoMenus> FindLastMenusId()
        {
            IEnumerator<RestoMenus> dataset = FindAll<RestoMenus>("SELECT * FROM Resto.resto_menus where reme_id =(SELECT IDENT_CURRENT('Resto.resto_menus'));");
            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }
        }
    }
}

