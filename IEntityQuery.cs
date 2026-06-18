using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Represents a query for entities.
	/// </summary>
	public interface IEntityQuery : IOrderedQueryable
	{
		/// <summary>
		/// The <see cref="IQueryProvider"/> of the native data access system.
		/// </summary>
		IQueryProvider NativeProvider { get; }

		/// <summary>
		/// The domain container which the query pertains to.
		/// </summary>
		IDomainContainer DomainContainer { get; }
	}

	/// <summary>
	/// Represents a query for entities of type <typeparamref name="E"/>.
	/// </summary>
	/// <typeparam name="E">The type of entities being queried.</typeparam>
	public interface IEntityQuery<E> : IEntityQuery, IOrderedQueryable<E>
	{
	}
}
