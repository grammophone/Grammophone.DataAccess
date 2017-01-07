using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Represents a reference property of an entity to another entity.
	/// </summary>
	/// <typeparam name="E">The type of the entity.</typeparam>
	/// <typeparam name="P">The type of the referenced entity.</typeparam>
	public interface IReferenceEntry<E, P> : IRelationEntry<E, P>
		where E : class
	{
		/// <summary>
		/// Returns the query that would be used to load this entity from the database.
		/// The returned query can be modified using LINQ to perform filtering 
		/// or operations in the database.
		/// </summary>
		IQueryable<P> Query();
	}
}
