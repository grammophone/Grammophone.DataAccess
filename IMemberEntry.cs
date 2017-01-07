using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Base interface for a scalar, complex type, reference or collection property
	/// of an entity.
	/// </summary>
	/// <typeparam name="E">The type of the entity.</typeparam>
	/// <typeparam name="P">The type of the property.</typeparam>
	public interface IMemberEntry<E, P>
		where E : class
	{
		/// <summary>
		/// The entity entry where this member entry belongs.
		/// </summary>
		IEntityEntry<E> EntityEntry { get; }

		/// <summary>
		/// The current value of the property.
		/// </summary>
		P CurrentValue { get; set; }

		/// <summary>
		/// The name of the property.
		/// </summary>
		string Name { get; }
	}
}
