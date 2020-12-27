using System;
using System.Threading.Tasks;
using System.Transactions;

namespace API.Base.Core.Extensions
{
    public static class TransactionHelper
    {
        public static async Task<bool> CreateAsyncTransactionScope(Func<Task<bool>> action, Func<Exception, Task> onError = null)
        {
            var transactionOption = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOption, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = await action.Invoke();
                    if (result)
                        transaction.Complete();
                    return result;
                }
            }
            catch (Exception exception)
            {
                if (onError != null) await onError.Invoke(exception);
                throw;
            }
        }
    }
}