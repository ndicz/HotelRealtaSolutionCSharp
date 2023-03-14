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
    internal class MenuPhotosRepository : RepositoryBase<MenuPhotos>, IMenuPhotosRepository
    {
        public MenuPhotosRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(MenuPhotos menuPhotos)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE Resto.resto_menu_photos set  remp_thumbnail_filename = @remp_thumbnail_filename," +
              "remp_photo_filename = @remp_photo_filename," +
              "remp_primary = @remp_primary," +
              "remp_url = @remp_url," +
              "remp_reme_id = @remp_reme_id " +
              " WHERE remp_id = @remp_id;",

                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@remp_thumbnail_filename",
                        DataType = DbType.String,
                        Value = menuPhotos.RempThumbnailFilename
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@remp_photo_filename",
                        DataType = DbType.String,
                        Value = menuPhotos.RempPhotoFilename
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@remp_primary",
                        DataType = DbType.Boolean,
                        Value = menuPhotos.RempPrimary

                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@remp_url",
                        DataType = DbType.String,
                        Value = menuPhotos.RempUrl
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@remp_reme_id",
                        DataType = DbType.Int32,
                        Value = menuPhotos.RempRemeId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@remp_id",
                        DataType = DbType.Int32,
                        Value = menuPhotos.RempId
                    }
                     }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();



        }

        public IEnumerable<MenuPhotos> FindAllMenuPhotos()
        {
            IEnumerator<MenuPhotos> dataSet = FindAll<MenuPhotos>("SELECT remp_id RempId," +
                "remp_thumbnail_filename RempThumbnailFilename," +
                "remp_photo_filename RempPhotoFilename, " +
                "remp_primary RempPrimary, " +
                "remp_url RempUrl," +
                "remp_reme_id RempRemeId " +
                " From Resto.resto_menu_photos");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public async Task<IEnumerable<MenuPhotos>> FindAllMenuPhotosAsync()
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Resto.resto_menu_photos;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] { }

            };
            IAsyncEnumerator<MenuPhotos> dataSet = FindAllAsync<MenuPhotos>(model);

            var item = new List<MenuPhotos>();


            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }


            return item;
        }
        public MenuPhotos FindMenuPhotosById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT * FROM Resto.resto_menu_photos where remp_id=@remp_id order by remp_id asc;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@remp_id",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };
            var dataSet = FindByCondition<MenuPhotos>(model);

            MenuPhotos? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }


            return item;
        }

        public void Insert(MenuPhotos menuPhotos)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO Resto.resto_menu_photos (remp_thumbnail_filename, remp_photo_filename, remp_primary, remp_url, remp_reme_id) " +
               "VALUES(@remp_thumbnail_filename, @remp_photo_filename, @remp_primary, @remp_url, @remp_reme_id)",

                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@remp_thumbnail_filename",
                        DataType = DbType.String,
                        Value = menuPhotos.RempThumbnailFilename
                    },
                       new SqlCommandParameterModel() {
                        ParameterName = "@remp_photo_filename",
                        DataType = DbType.String,
                        Value = menuPhotos.RempPhotoFilename
                    },
                       new SqlCommandParameterModel() {
                        ParameterName = "@remp_primary",
                        DataType = DbType.Boolean,
                        Value = menuPhotos.RempPrimary

                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@remp_url",
                        DataType = DbType.String,
                        Value = menuPhotos.RempUrl
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@remp_reme_id",
                        DataType = DbType.Int32,
                        Value = menuPhotos.RempRemeId
                    }
                      }

            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }


        public void Remove(MenuPhotos menuPhotos)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM Resto.resto_menu_photos WHERE remp_id = @remp_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@remp_id",
                        DataType = DbType.Int32,
                        Value = menuPhotos.RempId
                }
                }
            };

            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
        public IEnumerable<MenuPhotos> FindLastMenuPhotosId()
        {
            IEnumerator<MenuPhotos> dataset = FindAll<MenuPhotos>("SELECT * FROM Resto.resto_menu_photos where remp_id =(SELECT IDENT_CURRENT('Resto.resto_menu_photos'));");
            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }
        }
    }
}
