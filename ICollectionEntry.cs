using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Represents a collection property of an entity.
	/// </summary>
	/// <typeparam name="E">The type of the entity.</typeparam>
	/// <typeparam name="I">The type of items in the collection.</typeparam>
	public interface ICollectionEntry<E, I> : IRelationEntry<E, ICollection<I>>
		where E : class
		where I : class
	{
		/// <summary>
		/// Returns the query that would be used to load this collection from the database.
		/// The returned query can be modified using LINQ to perform filtering or operations in the database, 
		/// such as counting the number of entities in the collection in the database 
		/// without actually loading them.
		/// </summary>
		IQueryable<I> Query();
	}
}
