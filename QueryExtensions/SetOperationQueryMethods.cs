using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Grammophone.DataAccess.QueryExtensions
{
	/// <summary>
	/// Extension methods for set-based terminal mutation operations.
	/// </summary>
	public static class SetOperationQueryMethods
	{
		#region Private fields

		private static readonly SetOperationMethodsAdapter DefaultSetOperationMethodsAdapter = new SetOperationMethodsAdapter();

		#endregion

		#region Public methods

		/// <summary>
		/// Deletes all database rows selected by the query without materializing the corresponding entities.
		/// </summary>
		/// <typeparam name="T">The entity type.</typeparam>
		/// <param name="query">The query selecting the entities to delete.</param>
		/// <returns>The number of affected rows.</returns>
		/// <remarks>
		/// This operation executes immediately and bypasses change tracking. Already tracked entities are not synchronized.
		/// </remarks>
		public static int ExecuteDelete<T>(this IQueryable<T> query)
			where T : class
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetSetOperationMethodsAdapter(query).ExecuteDelete(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously deletes all database rows selected by the query without materializing the corresponding entities.
		/// </summary>
		/// <typeparam name="T">The entity type.</typeparam>
		/// <param name="query">The query selecting the entities to delete.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task whose result is the number of affected rows.</returns>
		/// <remarks>
		/// This operation executes immediately and bypasses change tracking. Already tracked entities are not synchronized.
		/// </remarks>
		public static Task<int> ExecuteDeleteAsync<T>(
			this IQueryable<T> query,
			CancellationToken cancellationToken = default(CancellationToken))
			where T : class
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetSetOperationMethodsAdapter(query).ExecuteDeleteAsync(
				QueryOperations.GetNativeQueryable(query),
				cancellationToken);
		}

		#endregion

		#region Private methods

		private static SetOperationMethodsAdapter GetSetOperationMethodsAdapter<T>(IQueryable<T> query)
		{
			if (query is IEntityQuery<T> entityQuery)
			{
				var queryTranslator = entityQuery.DomainContainer.TryGetQueryTranslator();

				if (queryTranslator != null)
				{
					return queryTranslator.SetOperationMethodsAdapter;
				}
			}

			return DefaultSetOperationMethodsAdapter;
		}

		#endregion
	}
}
