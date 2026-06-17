using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess.QueryExtensions
{
	/// <summary>
	/// Extension methods for producing results from queries.
	/// </summary>
	public static class TerminalQueryMethods
	{
		/// <summary>
		/// A fallback adapter when the implementation does not provide one.
		/// </summary>
		private static readonly TerminalMethodsAdapter DefaultTerminalMethodsAdapter = new DefaultTerminalMethodsAdapter();

		/// <summary>
		/// Asynchronously determines whether all the elements of a sequence satisfy a condition.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements to test for a condition.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains <see langword="true"/> if every element passes the test in <paramref name="predicate"/>; otherwise, <see langword="false"/>.
		/// </returns>
		public static Task<bool> AllAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			if (query is IEntityQuery<T> entityQuery)
			{
				var queryTranslator = entityQuery.DomainContainer.TryGetQueryTranslator();

				if (queryTranslator != null)
				{
					return queryTranslator.TerminalMethodsAdapter.AllAsync(entityQuery, predicate);
				}
			}

			return DefaultTerminalMethodsAdapter.AllAsync(query, predicate);
		}
	}
}
