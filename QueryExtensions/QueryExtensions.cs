using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Grammophone.DataAccess.QueryExtensions
{
	/// <summary>
	/// Extension methods for shaping queries.
	/// </summary>
	public static class QueryExtensions
	{
		#region Public methods

		/// <summary>
		/// Specifies the related objects to include in the query results.
		/// </summary>
		/// <typeparam name="T">The type of entity being queried.</typeparam>
		/// <param name="query">The source query.</param>
		/// <param name="path">The dot-separated list of related objects to return in the query results.</param>
		/// <returns>A new query with the defined include path.</returns>
		public static IQueryable<T> Include<T>(this IQueryable<T> query, string path)
			where T : class
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (path == null) throw new ArgumentNullException(nameof(path));

			var methodCallExpression = Expression.Call(
				null,
				QueryExtensionMethodInfos.IncludeString.MakeGenericMethod(typeof(T)),
				query.Expression,
				Expression.Constant(path));

			return query.Provider.CreateQuery<T>(methodCallExpression);
		}

		/// <summary>
		/// Specifies the related objects to include in the query results.
		/// </summary>
		/// <typeparam name="T">The type of entity being queried.</typeparam>
		/// <typeparam name="TProperty">The type of navigation property being included.</typeparam>
		/// <param name="query">The source query.</param>
		/// <param name="pathExpression">A lambda expression representing the path to include.</param>
		/// <returns>A new query with the defined include path.</returns>
		/// <remarks>
		/// Portable eager loading supports simple navigation paths. Provider-specific filtered include semantics are not part of this abstraction.
		/// </remarks>
		public static IIncludableEntityQuery<T, TProperty> Include<T, TProperty>(
			this IQueryable<T> query,
			Expression<Func<T, TProperty>> pathExpression)
			where T : class
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (pathExpression == null) throw new ArgumentNullException(nameof(pathExpression));
			if (!(query is IEntityQuery<T> entityQuery))
			{
				throw new NotSupportedException(
					"Portable typed Include requires a query originating from a Grammophone entity query.");
			}

			var methodCallExpression = Expression.Call(
				null,
				QueryExtensionMethodInfos.IncludeExpression.MakeGenericMethod(typeof(T), typeof(TProperty)),
				query.Expression,
				Expression.Quote(pathExpression));

			return new IncludableEntityQuery<T, TProperty>(entityQuery, methodCallExpression);
		}

		/// <summary>
		/// Specifies additional related objects to include through a previously included reference navigation.
		/// </summary>
		/// <typeparam name="T">The root entity type.</typeparam>
		/// <typeparam name="TPreviousProperty">The previously included property type.</typeparam>
		/// <typeparam name="TProperty">The type of navigation property being included.</typeparam>
		/// <param name="query">The includable source query.</param>
		/// <param name="pathExpression">A lambda expression representing the path to include.</param>
		/// <returns>A new includable query with the extended include path.</returns>
		public static IIncludableEntityQuery<T, TProperty> ThenInclude<T, TPreviousProperty, TProperty>(
			this IIncludableEntityQuery<T, TPreviousProperty> query,
			Expression<Func<TPreviousProperty, TProperty>> pathExpression)
			where T : class
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (pathExpression == null) throw new ArgumentNullException(nameof(pathExpression));

			var methodCallExpression = Expression.Call(
				null,
				QueryExtensionMethodInfos.ThenIncludeReference.MakeGenericMethod(
					typeof(T),
					typeof(TPreviousProperty),
					typeof(TProperty)),
				query.Expression,
				Expression.Quote(pathExpression));

			return new IncludableEntityQuery<T, TProperty>(query, methodCallExpression);
		}

		/// <summary>
		/// Specifies additional related objects to include through a previously included collection navigation.
		/// </summary>
		/// <typeparam name="T">The root entity type.</typeparam>
		/// <typeparam name="TPreviousProperty">The element type of the previously included collection property.</typeparam>
		/// <typeparam name="TProperty">The type of navigation property being included.</typeparam>
		/// <param name="query">The includable source query.</param>
		/// <param name="pathExpression">A lambda expression representing the path to include.</param>
		/// <returns>A new includable query with the extended include path.</returns>
		public static IIncludableEntityQuery<T, TProperty> ThenInclude<T, TPreviousProperty, TProperty>(
			this IIncludableEntityQuery<T, IEnumerable<TPreviousProperty>> query,
			Expression<Func<TPreviousProperty, TProperty>> pathExpression)
			where T : class
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (pathExpression == null) throw new ArgumentNullException(nameof(pathExpression));

			var methodCallExpression = Expression.Call(
				null,
				QueryExtensionMethodInfos.ThenIncludeCollection.MakeGenericMethod(
					typeof(T),
					typeof(TPreviousProperty),
					typeof(TProperty)),
				query.Expression,
				Expression.Quote(pathExpression));

			return new IncludableEntityQuery<T, TProperty>(query, methodCallExpression);
		}

		/// <summary>
		/// Returns a new query where the entities returned will not be cached in the container.
		/// </summary>
		/// <typeparam name="T">The type of entity being queried.</typeparam>
		/// <param name="query">The source query.</param>
		/// <returns>A new query with no-tracking behavior applied.</returns>
		public static IQueryable<T> AsNoTracking<T>(this IQueryable<T> query)
			where T : class
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			var methodCallExpression = Expression.Call(
				null,
				QueryExtensionMethodInfos.AsNoTracking.MakeGenericMethod(typeof(T)),
				query.Expression);

			return query.Provider.CreateQuery<T>(methodCallExpression);
		}

		#endregion
	}
}
