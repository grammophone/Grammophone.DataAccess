using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
		/// <param name="nativeQuery">The native query selecting the entities to delete.</param>
		/// <returns>The number of affected rows.</returns>
		/// <remarks>
		/// This is a set-based database operation. It does not materialize the selected entities, does not use the change tracker
		/// to mark individual entities as deleted and does not synchronize entities already tracked by the active domain container.
		/// </remarks>
		public virtual int ExecuteDelete<T>(IQueryable<T> nativeQuery)
			where T : class
		{
			throw CreateNotSupportedException(nameof(ExecuteDelete));
		}

		/// <summary>
		/// Asynchronously executes a set-based delete operation for the query.
		/// </summary>
		/// <typeparam name="T">The entity type.</typeparam>
		/// <param name="nativeQuery">The native query selecting the entities to delete.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task whose result is the number of affected rows.</returns>
		/// <remarks>
		/// This is a set-based database operation. It does not materialize the selected entities, does not use the change tracker
		/// to mark individual entities as deleted and does not synchronize entities already tracked by the active domain container.
		/// </remarks>
		public virtual Task<int> ExecuteDeleteAsync<T>(
			IQueryable<T> nativeQuery,
			CancellationToken cancellationToken = default(CancellationToken))
			where T : class
		{
			throw CreateNotSupportedException(nameof(ExecuteDeleteAsync));
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
