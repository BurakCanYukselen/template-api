using System;
using System.Threading.Tasks;
using System.Transactions;

namespace API.Base.Core.Extensions
{
    public static class TransactionHelper
    {
        public static async Task CreateAsyncTransactionScope(Func<Task> action, Func<Exception,Task> onError = null)
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
                    await action.Invoke();
                    transaction.Complete();
                }
            }
            catch (Exception exception)
            {
                await onError?.Invoke(exception);
            }
        }
    }
}