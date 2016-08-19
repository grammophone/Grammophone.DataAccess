using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Represents a query for entities of type <typeparamref name="E"/>.
	/// </summary>
	/// <typeparam name="E">The type of entities being queried.</typeparam>
	public interface IEntityQuery<E> : IOrderedQueryable<E>
		where E : class
	{
		/// <summary>
		/// Returns a new query where the entities returned will not be cached in the
		/// container.
		/// </summary>
		/// <returns>A new query with NoTracking applied.</returns>
		IEntityQuery<E> AsNoTracking();

		/// <summary>
		/// Specifies the related objects to include in the query results.
		/// </summary>
		/// <param name="path">
		/// The dot-separated list of related objects to return in the query results.
		/// </param>
		/// <returns>
		/// A new <see cref="IEntityQuery{E}"/>> with the defined query path.
		/// </returns>
		IEntityQuery<E> Include(string path);

		/// <summary>
		/// Specifies the related objects to include in the query results.
		/// </summary>
		/// <typeparam name="P">The type of navigation property being included.</typeparam>
		/// <param name="pathExpression">A lambda expression representing the path to include.</param>
		/// <returns>A new <see cref="IQueryable{E}"/> with the defined query path.</returns>
		IQueryable<E> Include<P>(Expression<Func<E, P>> pathExpression);
	}
}
