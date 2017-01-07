using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Represents a scalar or complex type property
	/// of an entity.
	/// </summary>
	/// <typeparam name="E">The type of the entity.</typeparam>
	/// <typeparam name="P">The type of the property.</typeparam>
	public interface IPropertyEntry<E, P> : IMemberEntry<E, P>
		where E : class
	{
		/// <summary>
		/// The original value of the property, before any modification.
		/// </summary>
		P OriginalValue { get; }

		/// <summary>
		/// If true, the property is marked as modified.
		/// </summary>
		bool IsModified { get; set; }
	}
}
