using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Extensions
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
    }
}
