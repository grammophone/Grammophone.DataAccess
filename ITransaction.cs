using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Represents a transaction as an <see cref="IDisposable"/>.
	/// The transaction must be <see cref="Commit"/>ted
	/// before being <see cref="IDisposable.Dispose"/>d, else it is rolled back.
	/// Async and await is supported.
	/// </summary>
	/// <remarks>
	/// The <see cref="ITransaction"/> interface simulates nested transactions.
	/// If this is a nested transaction, the <see cref="IDomainContainer.SaveChanges"/>
	/// and <see cref="IDomainContainer.SaveChangesAsync()"/> do nothing, and
	/// failing to call <see cref="ITransaction.Commit"/> casts a vote for rollback
	/// for the whole operation composition. As a consequence, do not rely on 
	/// autoincrement IDs being generated after SaveChanges.
	/// </remarks>
	public interface ITransaction : IDisposable
	{
		/// <summary>
		/// Marks the transaction as valid for commit.
		/// Actual committing takes place when all nested transactions are
		/// disposed and marked as committed.
		/// If this method or <see cref="CommitAsync()"/> or <see cref="Pass"/> has not been called when
		/// method <see cref="IDisposable.Dispose"/> is invoked, the
		/// transaction is marked for rollback. A <see cref="IDomainContainer.SaveChanges"/>
		/// call is implied calling this method when the transaction
		/// is not marked for rollback.
		/// </summary>
		void Commit();

		/// <summary>
		/// Marks the transaction as valid for commit.
		/// Actual committing takes place when all nested transactions are
		/// disposed and marked as committed.
		/// If this method or <see cref="Commit"/> or <see cref="Pass"/> has not been called when
		/// method <see cref="IDisposable.Dispose"/> is invoked, the
		/// transaction is marked for rollback. A <see cref="IDomainContainer.SaveChangesAsync()"/>
		/// call is implied calling this method when the transaction
		/// is not marked for rollback.
		/// </summary>
		Task CommitAsync();

		/// <summary>
		/// Marks the transaction as valid for commit.
		/// Actual committing takes place when all nested transactions are
		/// disposed and marked as committed.
		/// If this method or <see cref="Commit"/> or <see cref="Pass"/> has not been called when
		/// method <see cref="IDisposable.Dispose"/> is invoked, the
		/// transaction is marked for rollback. A <see cref="IDomainContainer.SaveChangesAsync()"/>
		/// call is implied calling this method when the transaction
		/// is not marked for rollback.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token for the operation.</param>
		Task CommitAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Marks the transaction valid for commit but does not save.
		/// Prevents rollback of higher nesting transactions;
		/// thus passes the decision whether to save to the higher transactions.
		/// If this method or <see cref="Commit"/> or <see cref="CommitAsync()"/> has not been called when
		/// method <see cref="IDisposable.Dispose"/> is invoked, the
		/// transaction is marked for rollback.
		/// </summary>
		void Pass();

		/// <summary>
		/// Fired when the whole transaction is committed successfully.
		/// </summary>
		event Action Succeeding;

		/// <summary>
		/// Fired when the whole transaction is rolled back.
		/// </summary>
		event Action RollingBack;
	}
}
