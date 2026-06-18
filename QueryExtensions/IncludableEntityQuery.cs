using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Grammophone.DataAccess.QueryExtensions
{
	/// <summary>
	/// Default implementation of <see cref="IIncludableEntityQuery{TEntity, TProperty}"/>.
	/// </summary>
	/// <typeparam name="TEntity">The root entity type.</typeparam>
	/// <typeparam name="TProperty">The last included property type.</typeparam>
	public class IncludableEntityQuery<TEntity, TProperty> : IIncludableEntityQuery<TEntity, TProperty>
	{
		#region Private fields

		private readonly IEntityQuery<TEntity> sourceQuery;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="sourceQuery">The source entity query.</param>
		/// <param name="expression">The portable include expression.</param>
		public IncludableEntityQuery(IEntityQuery<TEntity> sourceQuery, Expression expression)
		{
			if (sourceQuery == null) throw new ArgumentNullException(nameof(sourceQuery));
			if (expression == null) throw new ArgumentNullException(nameof(expression));

			this.sourceQuery = sourceQuery;
			this.Expression = expression;
		}

		#endregion

		#region Public properties

		/// <inheritdoc/>
		public IQueryProvider NativeProvider => sourceQuery.NativeProvider;

		/// <inheritdoc/>
		public IDomainContainer DomainContainer => sourceQuery.DomainContainer;

		#endregion

		#region Explicit IQueryable implementation

		/// <inheritdoc/>
		public Type ElementType => typeof(TEntity);

		/// <inheritdoc/>
		public Expression Expression { get; }

		/// <inheritdoc/>
		public IQueryProvider Provider => sourceQuery.Provider;

		#endregion

		#region IEnumerable<TEntity> implementation

		/// <inheritdoc/>
		public IEnumerator<TEntity> GetEnumerator()
		{
			return Provider.CreateQuery<TEntity>(Expression).GetEnumerator();
		}

		#endregion

		#region Explicit IEnumerable implementation

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}
