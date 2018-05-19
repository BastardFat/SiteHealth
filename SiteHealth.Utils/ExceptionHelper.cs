using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteHealth.Utils
{
    public class ExceptionHelper
    {
        public Exception GetErrorFromAction(Action action)
        {
            try
            {
                action();
                return null;
            }
            catch (Exception ex)
            {
                return SimplifyException(ex);
            }
        }

        
        public async Task<Exception> GetErrorFromActionAsync(Func<Task> action)
        {
            try
            {
                await action();
                return null;
            }
            catch (Exception ex)
            {
                return SimplifyException(ex);
            }
        }

        public async Task SimplifyExceptionForActionAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                throw SimplifyException(ex);
            }
        }

        public async Task<T> SimplifyExceptionForActionAsync<T>(Func<Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                throw SimplifyException(ex);
            }
        }

        private static Exception SimplifyException(Exception ex)
        {
            if (ex is AggregateException)
            {
                ex = (ex as AggregateException).InnerExceptions.First();
            }
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }

    }
}
