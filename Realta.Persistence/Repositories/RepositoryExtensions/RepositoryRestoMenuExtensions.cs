using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Realta.Domain.Entities;
using System.Reflection;

namespace Realta.Persistence.Repositories.RepositoryExtensions
{
    public static class RepositoryRestoMenuExtensions
    {

        public static IQueryable<RestoMenus> SearchRestoMenus(this IQueryable<RestoMenus> restoMenus, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return restoMenus;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return restoMenus.Where(p => p.RemeName.ToLower().Contains(lowerCaseSearchTerm));

        }
        public static IQueryable<RestoMenus> Sort(this IQueryable<RestoMenus> restoMenus, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return restoMenus.OrderBy(e => e.RemeName);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(RestoMenus).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return restoMenus.OrderBy(e => e.RemeName);

            return restoMenus.OrderBy(orderQuery);
        }

    }
}
