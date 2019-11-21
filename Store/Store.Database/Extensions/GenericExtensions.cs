using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Database.Entities.Base;

namespace Store.Database.Extensions
{
    public static class GenericExtensions
    {
        public static IEnumerable<T> SingleAsEnumerable<T>(this T value)
        {
            yield return value;
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action reaction, Predicate<T> condition = null)
        {
            if (list != null)
            {
                foreach (T item in list)
                {
                    if (condition == null || condition(item))
                        reaction();
                }
            }
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> e, Func<T, IEnumerable<T>> childrenSelector)
        {
            return e.SelectMany(c => childrenSelector(c).Flatten(childrenSelector)).Concat(e);
        }

        public static async Task<bool> DoWithRetry(this Func<Task> action, Action<Exception, int> exceptionHandler,
            int maxRetryCount = 1)
        {
            for (int i = 0; i <= maxRetryCount; i++)
            {
                try
                {
                    await action();

                    return true;
                }
                catch (Exception exc)
                {
                    exceptionHandler(exc, i);
                }
            }

            return false;
        }


        public async static Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query,
            int page, int pageSize, CancellationToken cancellationToken) where T : IEntity
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize)
                .ToArrayAsync(cancellationToken);

            return result;
        }

    }
}
