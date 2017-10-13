// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Threading.Tasks;
using XamarinSample.Core.Utils;

namespace XamarinSample.Core.Data
{
    public static class DbOperationsWrappers
    {
        public static async Task<ErrorOr<TSuccess>> PerformSafelyAsync<TSuccess>(MobileDbContext mobileDbContext, Func<MobileDbContext, Task<ErrorOr<TSuccess>>> operation)
            where TSuccess : class 
        {
            try
            {
                return await operation.Invoke(mobileDbContext);
            }
            catch (OperationCanceledException operationCanceledException)
            {
                return new ErrorOr<TSuccess>(new ErrorModel(operationCanceledException));
            }
            catch (Exception exception)
            {
                return new ErrorOr<TSuccess>(new ErrorModel(exception));
            }
        }
    }
}
