using System;
using System.Linq;
using System.Linq.Expressions;
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
		/// This operation executes immediately as a set-based database operation. It does not materialize the selected entities,
		/// does not use the change tracker to mark individual entities as deleted and does not synchronize entities already tracked
		/// by the active domain container.
		/// </remarks>
		public static int ExecuteDelete<T>(this IQueryable<T> query)
			where T : class
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetSetOperationMethodsAdapter(query, out var domainContainer).ExecuteDelete(
				domainContainer,
				QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously deletes all database rows selected by the query without materializing the corresponding entities.
		/// </summary>
		/// <typeparam name="T">The entity type.</typeparam>
		/// <param name="query">The query selecting the entities to delete.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task whose result is the number of affected rows.</returns>
		/// <remarks>
		/// This operation executes immediately as a set-based database operation. It does not materialize the selected entities,
		/// does not use the change tracker to mark individual entities as deleted and does not synchronize entities already tracked
		/// by the active domain container.
		/// </remarks>
		public static Task<int> ExecuteDeleteAsync<T>(
			this IQueryable<T> query,
			CancellationToken cancellationToken = default(CancellationToken))
			where T : class
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetSetOperationMethodsAdapter(query, out var domainContainer).ExecuteDeleteAsync(
				domainContainer,
				QueryOperations.GetNativeQueryable(query),
				cancellationToken);
		}

		/// <summary>
		/// Updates all database rows selected by the query without materializing the corresponding entities.
		/// </summary>
		/// <typeparam name="T">The entity type.</typeparam>
		/// <param name="query">The query selecting the entities to update.</param>
		/// <param name="setPropertyCalls">The property update specification.</param>
		/// <returns>The number of affected rows.</returns>
		/// <remarks>
		/// This operation executes immediately as a set-based database operation. It does not materialize the selected entities,
		/// does not use the change tracker to update individual entities and does not synchronize entities already tracked
		/// by the active domain container.
		/// </remarks>
		public static int ExecuteUpdate<T>(
			this IQueryable<T> query,
			Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls)
			where T : class
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (setPropertyCalls == null) throw new ArgumentNullException(nameof(setPropertyCalls));

			return GetSetOperationMethodsAdapter(query, out var domainContainer).ExecuteUpdate(
				domainContainer,
				QueryOperations.GetNativeQueryable(query),
				QueryOperations.TranslateExpression(query, setPropertyCalls));
		}

		/// <summary>
		/// Asynchronously updates all database rows selected by the query without materializing the corresponding entities.
		/// </summary>
		/// <typeparam name="T">The entity type.</typeparam>
		/// <param name="query">The query selecting the entities to update.</param>
		/// <param name="setPropertyCalls">The property update specification.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task whose result is the number of affected rows.</returns>
		/// <remarks>
		/// This operation executes immediately as a set-based database operation. It does not materialize the selected entities,
		/// does not use the change tracker to update individual entities and does not synchronize entities already tracked
		/// by the active domain container.
		/// </remarks>
		public static Task<int> ExecuteUpdateAsync<T>(
			this IQueryable<T> query,
			Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls,
			CancellationToken cancellationToken = default(CancellationToken))
			where T : class
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (setPropertyCalls == null) throw new ArgumentNullException(nameof(setPropertyCalls));

			return GetSetOperationMethodsAdapter(query, out var domainContainer).ExecuteUpdateAsync(
				domainContainer,
				QueryOperations.GetNativeQueryable(query),
				QueryOperations.TranslateExpression(query, setPropertyCalls),
				cancellationToken);
		}

		#endregion

		#region Private methods

		private static SetOperationMethodsAdapter GetSetOperationMethodsAdapter<T>(IQueryable<T> query, out IDomainContainer domainContainer)
		{
			if (query is IEntityQuery<T> entityQuery)
			{
				domainContainer = entityQuery.DomainContainer;

				var queryTranslator = domainContainer.TryGetQueryTranslator();

				if (queryTranslator != null)
				{
					return queryTranslator.SetOperationMethodsAdapter;
				}
			}
			else
			{
				domainContainer = null;
			}

			return DefaultSetOperationMethodsAdapter;
		}

		#endregion
	}
}
