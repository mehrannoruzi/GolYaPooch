using Elk.Core;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace GolPooch.DataAccess.Ef
{
    public class DbContextInterceptors : DbCommandInterceptor
    {
        public override Task<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            return base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
        }

        public override Task CommandFailedAsync(DbCommand command, CommandErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            return base.CommandFailedAsync(command, eventData, cancellationToken);
        }

        public override Task<int> NonQueryExecutedAsync(DbCommand command, CommandExecutedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            return base.NonQueryExecutedAsync(command, eventData, result, cancellationToken);
        }

        public override Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }

        public override Task<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
        {
            return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
        }

        public override Task<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
        {
            return base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
        }

        public override Task<object> ScalarExecutedAsync(DbCommand command, CommandExecutedEventData eventData, object result, CancellationToken cancellationToken = default)
        {
            return base.ScalarExecutedAsync(command, eventData, result, cancellationToken);
        }

        public override InterceptionResult<DbCommand> CommandCreating(CommandCorrelatedEventData eventData, InterceptionResult<DbCommand> result)
        {
            //result.Result.ToPersianCharacters();
            //result.Result.ToEnglishNumber();

            return base.CommandCreating(eventData, result);
        }

        public override DbCommand CommandCreated(CommandEndEventData eventData, DbCommand result)
        {
            result.ToPersianCharacters();
            result.ToEnglishNumber();

            return base.CommandCreated(eventData, result);
        }

        public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
        {
            command.ToPersianCharacters();
            command.ToEnglishNumber();

            return base.NonQueryExecuted(command, eventData, result);
        }

        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            command.ToPersianCharacters();
            command.ToEnglishNumber();

            return base.ReaderExecuted(command, eventData, result);
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            command.ToPersianCharacters();
            command.ToEnglishNumber();

            return base.ReaderExecuting(command, eventData, result);
        }

    }
}
