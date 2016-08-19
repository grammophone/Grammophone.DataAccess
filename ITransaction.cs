using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		/// If this method has not been called when 
		/// method <see cref="IDisposable.Dispose"/> is invoked, the
		/// transaction is marked for rollback. If <see cref="IDomainContainer.SaveChanges"/>
		/// or <see cref="IDomainContainer.SaveChangesAsync()"/> haven't been called 
		/// until the uppermost commit, a <see cref="IDomainContainer.SaveChanges"/> is
		/// issued.
		/// </summary>
		void Commit();

		/// <summary>
		/// Marks the transaction as valid for commit.
		/// Actual committing takes place when all nested transactions are
		/// disposed and marked as committed.
		/// If this method has not been called when 
		/// method <see cref="IDisposable.Dispose"/> is invoked, the
		/// transaction is marked for rollback. If <see cref="IDomainContainer.SaveChanges"/>
		/// or <see cref="IDomainContainer.SaveChangesAsync()"/> haven't been called 
		/// until the uppermost commit, a <see cref="IDomainContainer.SaveChangesAsync()"/> is
		/// issued.
		/// </summary>
		Task CommitAsync();

		/// <summary>
		/// Fired when the whole transaction is committed successfully.
		/// </summary>
		event Action Succeeding;

		/// <summary>
		/// Fired when the whole transaction is committed successfully.
		/// </summary>
		event Action RollingBack;
	}
}
