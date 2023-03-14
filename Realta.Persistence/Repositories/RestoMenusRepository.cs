using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Domain.RequestFeatures;
using Realta.Persistence.Base;
using Realta.Persistence.Repositories.RepositoryExtensions;
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
                //CommandText = "UPDATE Resto.resto_menus set  reme_name = @reme_name," +
                //"reme_description = @reme_desc," +
                //"reme_price = @reme_price," +
                //"reme_status = @reme_status," +
                //"reme_modified_date =" +
                //" @reme_mod WHERE reme_id = @reme_id;",

                CommandText = "Resto.SpUpdateRestoMenus",

                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@reme_name",
                        DataType = DbType.String,
                        Value = restoMenus.RemeName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@reme_desc",
                        DataType = DbType.String,
                        Value = restoMenus.RemeDescription
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@reme_price",
                        DataType= DbType.Decimal,
                        Value = restoMenus.RemePrice
                    },
                        new SqlCommandParameterModel() {
                        ParameterName = "@reme_status",
                        DataType = DbType.String,
                        Value = restoMenus.RemeStatus
                    },
                        new SqlCommandParameterModel() {
                        ParameterName = "@reme_mod",
                        DataType = DbType.DateTime,
                        Value = restoMenus.RemeModifiedDate
                    },

                        new SqlCommandParameterModel() {
                        ParameterName = "@reme_id",
                        DataType = DbType.Int32,
                        Value = restoMenus.RemeId
                    }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<RestoMenus> FindAllRestoMenus()
        {
            IEnumerator<RestoMenus> dataSet = FindAll<RestoMenus>("Select reme_faci_id RemeFaciId, " +
                "reme_id RemeId, reme_name RemeName, " +
                "reme_description RemeDescription, " +
                "reme_price RemePrice, reme_status RemeStatus, " +
                "reme_modified_date RemeModifiedDate, " +
                "reme_type RemeType from Resto.resto_menus");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }

            //var dataSet = GetAll<RestoMenus>("SELECT  * From Resto.resto_menus");

            //return dataSet.ToList();

            //return GetAll<RestoMenus>("SELECT  * From Resto.resto_menus");
        }

        public async Task<IEnumerable<RestoMenus>> FindAllRestoAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"SELECT 
                                reme_faci_id as RemeFaciId
                                FROM Resto.resto_menus;",
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
                CommandText = "Select reme_faci_id RemeFaciId, " +
                "reme_id RemeId, reme_name RemeName, " +
                "reme_description RemeDescription, " +
                "reme_price RemePrice, reme_status RemeStatus, " +
                "reme_modified_date RemeModifiedDate, " +
                "reme_type RemeType from Resto.resto_menus where reme_id=@reme_id order by reme_id asc;",
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
                CommandText = "INSERT INTO Resto.resto_menus (reme_faci_id,reme_name,reme_description,reme_price,reme_status,reme_modified_date,reme_type) " +
               "VALUES(@reme_faci_id,@reme_name,@reme_desc,@reme_price,@reme_status,@reme_mod,@reme_type)",

                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@reme_faci_id",
                        DataType = DbType.Int32,
                        Value = restoMenus.RemeFaciId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@reme_name",
                        DataType = DbType.String,
                        Value = restoMenus.RemeName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@reme_desc",
                        DataType = DbType.String,
                        Value = restoMenus.RemeDescription
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@reme_price",
                        DataType= DbType.Decimal,
                        Value = restoMenus.RemePrice
                    },
                    new SqlCommandParameterModel() {
                    ParameterName = "@reme_status",
                    DataType = DbType.String,
                    Value = restoMenus.RemeStatus
                    },
                    new SqlCommandParameterModel() {
                    ParameterName = "@reme_mod",
                    DataType = DbType.DateTime,
                    Value = restoMenus.RemeModifiedDate
                    },
                    new SqlCommandParameterModel() {
                    ParameterName = "@reme_type",
                    DataType = DbType.String,
                    Value = restoMenus.RemeType
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
                        Value = restoMenus.RemeId
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

        public int GetIdSequence()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT IDENT_CURRENT('resto.resto_menus');",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }
            };

            decimal id = _adoContext.ExecuteScalar<decimal>(model);
            _adoContext.Dispose();
            return (int)id;
        }



        public async Task<IEnumerable<RestoMenus>> GetRestoMenuPaging(RestoMenusParameters restoMenusParameters)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "Select reme_faci_id RemeFaciId, " +
                "reme_id RemeId, reme_name RemeName, " +
                "reme_description RemeDescription, " +
                "reme_price RemePrice, reme_status RemeStatus, " +
                "reme_modified_date RemeModifiedDate, " +
                "reme_type RemeType FROM resto.resto_menus order by reme_id OFFSET @pageNo ROWS FETCH NEXT  @pageSize ROWS ONLY;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                            ParameterName = "@pageNo",
                            DataType = DbType.Int32,
                            Value = restoMenusParameters.PageNumber
                        },
                     new SqlCommandParameterModel() {
                            ParameterName = "@pageSize",
                            DataType = DbType.Int32,
                            Value = restoMenusParameters.PageSize
                        }
                }

            };

            IAsyncEnumerator<RestoMenus> dataSet = FindAllAsync<RestoMenus>(model);

            var item = new List<RestoMenus>();

            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }

            return item;
        }

        public async Task<PagedList<RestoMenus>> GetRestoMenuPagelist(RestoMenusParameters restoMenusParameters)
        {

            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText =
                            "Select reme_faci_id RemeFaciId, " +
                            "reme_id RemeId, reme_name RemeName, " +
                            "reme_description RemeDescription, " +
                            "reme_price RemePrice, reme_status RemeStatus, " +
                            "reme_modified_date RemeModifiedDate, " +
                            "reme_type RemeType FROM resto.resto_menus order by reme_id ",
                            //"OFFSET @pageNo ROWS FETCH NEXT @pageSize ROWS ONLY",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                            ParameterName = "@pageNo",
                            DataType = DbType.Int32,
                            Value = restoMenusParameters.PageNumber
                        },
                     new SqlCommandParameterModel() {
                            ParameterName = "@pageSize",
                            DataType = DbType.Int32,
                            Value = restoMenusParameters.PageSize
                        }
                }

            };

            var restoMenus = await GetAllAsync<RestoMenus>(model);
            var totalRow = FindAllRestoMenus().Count();

            //var restoSearch = restoMenus.Where(
            //    p => p.RemeName.ToLower().Contains(
            //        restoMenusParameters.SearchTerm == null ? "" : restoMenusParameters.SearchTerm.Trim().ToLower()
            //        )
            //    ); 

            var restoSearch = restoMenus.AsQueryable().SearchRestoMenus(restoMenusParameters.SearchTerm).Sort(restoMenusParameters.orderBy);

            //return new PagedList<Product>(products.ToList(), totalRow, productParameters.PageNumber, productParameters.PageSize);
            //return new PagedList<RestoMenus>(restoSearch.ToList(), totalRow, restoMenusParameters.PageNumber, restoMenusParameters.PageSize);
            return PagedList<RestoMenus>.ToPagedList(restoSearch.ToList(), restoMenusParameters.PageNumber, restoMenusParameters.PageSize);
        }

    }
}



