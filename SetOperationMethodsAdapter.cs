using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Grammophone.DataAccess.QueryExtensions;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Contract for adapting set-based terminal mutation methods.
	/// </summary>
	public class SetOperationMethodsAdapter
	{
		#region Public methods

		/// <summary>
		/// Executes a set-based delete operation for the query.
		/// </summary>
		/// <typeparam name="T">The entity type.</typeparam>
		/// <param name="domainContainer">The domain container executing the operation.</param>
		/// <param name="nativeQuery">The native query selecting the entities to delete.</param>
		/// <returns>The number of affected rows.</returns>
		/// <remarks>
		/// This is a set-based database operation. It does not materialize the selected entities, does not use the change tracker
		/// to mark individual entities as deleted and does not synchronize entities already tracked by the active domain container.
		/// </remarks>
		public virtual int ExecuteDelete<T>(IDomainContainer domainContainer, IQueryable<T> nativeQuery)
			where T : class
		{
			throw CreateNotSupportedException(nameof(ExecuteDelete));
		}

		/// <summary>
		/// Asynchronously executes a set-based delete operation for the query.
		/// </summary>
		/// <typeparam name="T">The entity type.</typeparam>
		/// <param name="domainContainer">The domain container executing the operation.</param>
		/// <param name="nativeQuery">The native query selecting the entities to delete.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task whose result is the number of affected rows.</returns>
		/// <remarks>
		/// This is a set-based database operation. It does not materialize the selected entities, does not use the change tracker
		/// to mark individual entities as deleted and does not synchronize entities already tracked by the active domain container.
		/// </remarks>
		public virtual Task<int> ExecuteDeleteAsync<T>(
			IDomainContainer domainContainer,
			IQueryable<T> nativeQuery,
			CancellationToken cancellationToken = default(CancellationToken))
			where T : class
		{
			throw CreateNotSupportedException(nameof(ExecuteDeleteAsync));
		}

		/// <summary>
		/// Executes a set-based update operation for the query.
		/// </summary>
		/// <typeparam name="T">The entity type.</typeparam>
		/// <param name="domainContainer">The domain container executing the operation.</param>
		/// <param name="nativeQuery">The native query selecting the entities to update.</param>
		/// <param name="setPropertyCalls">The property update specification.</param>
		/// <returns>The number of affected rows.</returns>
		/// <remarks>
		/// This is a set-based database operation. It does not materialize the selected entities, does not use the change tracker
		/// to update individual entities and does not synchronize entities already tracked by the active domain container.
		/// </remarks>
		public virtual int ExecuteUpdate<T>(
			IDomainContainer domainContainer,
			IQueryable<T> nativeQuery,
			Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls)
			where T : class
		{
			throw CreateNotSupportedException(nameof(ExecuteUpdate));
		}

		/// <summary>
		/// Asynchronously executes a set-based update operation for the query.
		/// </summary>
		/// <typeparam name="T">The entity type.</typeparam>
		/// <param name="domainContainer">The domain container executing the operation.</param>
		/// <param name="nativeQuery">The native query selecting the entities to update.</param>
		/// <param name="setPropertyCalls">The property update specification.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task whose result is the number of affected rows.</returns>
		/// <remarks>
		/// This is a set-based database operation. It does not materialize the selected entities, does not use the change tracker
		/// to update individual entities and does not synchronize entities already tracked by the active domain container.
		/// </remarks>
		public virtual Task<int> ExecuteUpdateAsync<T>(
			IDomainContainer domainContainer,
			IQueryable<T> nativeQuery,
			Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls,
			CancellationToken cancellationToken = default(CancellationToken))
			where T : class
		{
			throw CreateNotSupportedException(nameof(ExecuteUpdateAsync));
		}

		#endregion

		#region Private methods

		private static DataAccessException CreateNotSupportedException(string methodName)
		{
			return new DataAccessException(
				$"Set operation '{methodName}' is not supported by the current query provider. Use a provider implementation or extension package that supports set-based mutations.");
		}

		#endregion
	}
}
