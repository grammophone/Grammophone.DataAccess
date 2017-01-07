using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Represents a complex property of an entity.
	/// </summary>
	/// <typeparam name="E">The type of the entity.</typeparam>
	/// <typeparam name="P">The type of the complex property.</typeparam>
	public interface IComplexPropertyEntry<E, P> : IPropertyEntry<E, P>
		where E : class
	{
		/// <summary>
		/// Gets an object that represents a scalar or complex subproperty of this complex property.
		/// </summary>
		/// <typeparam name="N">The type of the subproperty.</typeparam>
		/// <param name="subpropertySelector">An expression representing the subproperty.</param>
		/// <returns>Returns an object representing the subproperty.</returns>
		IPropertyEntry<E, N> Property<N>(Expression<Func<P, N>> subpropertySelector);

		/// <summary>
		/// Gets an object that represents a complex subproperty of this complex property.
		/// </summary>
		/// <typeparam name="N">The type of the subproperty.</typeparam>
		/// <param name="subpropertySelector">An expression representing the subproperty.</param>
		/// <returns>Returns an object representing the subproperty.</returns>
		IComplexPropertyEntry<E, N> ComplexProperty<N>(Expression<Func<P, N>> subpropertySelector);
	}
}
