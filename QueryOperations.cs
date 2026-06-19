using System;
using System.Linq;
using System.Linq.Expressions;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Common query operations used by terminal and shaping method adapters.
	/// </summary>
	public static class QueryOperations
	{
		#region Public methods

		/// <summary>
		/// Get a native queryable from an abstraction queryable.
		/// </summary>
		/// <typeparam name="T">The query element type.</typeparam>
		/// <param name="queryable">The queryable.</param>
		/// <returns>Returns a native queryable when the input is an abstraction query; else returns <paramref name="queryable"/>.</returns>
		public static IQueryable<T> GetNativeQueryable<T>(IQueryable<T> queryable)
		{
			if (queryable == null) throw new ArgumentNullException(nameof(queryable));

			switch (queryable)
			{
				case IEntityQuery<T> entityQuery:
					var expression = TranslateExpression(entityQuery, entityQuery.Expression);

					return entityQuery.NativeProvider.CreateQuery<T>(expression);

				default:
					return queryable;
			}
		}

		/// <summary>
		/// Translate an expression using the queryable's translating provider, if present.
		/// </summary>
		/// <typeparam name="TShape">The query element type.</typeparam>
		/// <typeparam name="TExpression">The expression type.</typeparam>
		/// <param name="queryable">The queryable which supplies the translating provider.</param>
		/// <param name="expression">The expression to translate.</param>
		/// <returns>The translated expression when the queryable has a translating provider; else <paramref name="expression"/>.</returns>
		public static TExpression TranslateExpression<TShape, TExpression>(IQueryable<TShape> queryable, TExpression expression)
			where TExpression : Expression
		{
			if (queryable == null) throw new ArgumentNullException(nameof(queryable));
			if (expression == null) throw new ArgumentNullException(nameof(expression));

			switch (queryable)
			{
				case IEntityQuery<TShape> entityQuery:
					return TranslateExpression(entityQuery, expression);

				default:
					return expression;
			}
		}

		#endregion

		#region Private methods

		private static TExpression TranslateExpression<TShape, TExpression>(IEntityQuery<TShape> entityQuery, TExpression expression)
			where TExpression : Expression
		{
			switch (entityQuery.Provider)
			{
				case TranslatingQueryProvider translatingQueryProvider:
					return (TExpression)translatingQueryProvider.TranslateExpression(expression);

				default:
					return expression;
			}
		}

		#endregion
	}
}
