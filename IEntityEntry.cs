using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Provides information about and control of an entity 
	/// tracked by <see cref="IDomainContainer"/>.
	/// </summary>
	/// <typeparam name="E">The type of the entity.</typeparam>
	public interface IEntityEntry<E>
		where E : class
	{
		/// <summary>
		/// The entity.
		/// </summary>
		E Entity { get; }

		/// <summary>
		/// The state of the <see cref="Entity"/>.
		/// </summary>
		TrackingState State { get; set; }

		/// <summary>
		/// Reloads the entity from the database overwriting any property 
		/// values with values from the database.
		/// The entity will be in the <see cref="TrackingState.Unchanged"/> state after calling this method.
		/// </summary>
		void Reload();

		/// <summary>
		/// Asynchronously reloads the entity from the database overwriting any property 
		/// values with values from the database.
		/// The entity will be in the <see cref="TrackingState.Unchanged"/> state after calling this method.
		/// </summary>
		/// <returns>Returns a task completing the action.</returns>
		Task ReloadAsync();

		/// <summary>
		/// Asynchronously reloads the entity from the database overwriting any property 
		/// values with values from the database.
		/// The entity will be in the <see cref="TrackingState.Unchanged"/> state after calling this method.
		/// </summary>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>Returns a task completing the action.</returns>
		Task ReloadAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Gets an object that represents a scalar or complex property of this entity.
		/// </summary>
		/// <typeparam name="P">The type of the property.</typeparam>
		/// <param name="propertySelector">An expression representing the property.</param>
		/// <returns>Returns an object representing the property.</returns>
		IPropertyEntry<E, P> Property<P>(Expression<Func<E, P>> propertySelector);

		/// <summary>
		/// Gets an object that represents a complex property of this entity.
		/// </summary>
		/// <typeparam name="P">The type of the property.</typeparam>
		/// <param name="propertySelector">An expression representing the property.</param>
		/// <returns>Returns an object representing the property.</returns>
		IComplexPropertyEntry<E, P> ComplexProperty<P>(Expression<Func<E, P>> propertySelector);

		/// <summary>
		/// Gets an object that represents a reference property of this entity.
		/// </summary>
		/// <typeparam name="P">The type of the property.</typeparam>
		/// <param name="propertySelector">An expression representing the property.</param>
		/// <returns>Returns an object representing the property.</returns>
		IReferenceEntry<E, P> Reference<P>(Expression<Func<E, P>> propertySelector) where P : class;

		/// <summary>
		/// Gets an object that represents a collection property of this entity.
		/// </summary>
		/// <typeparam name="I">The type of items in the collection.</typeparam>
		/// <param name="propertySelector">An expression representing the property.</param>
		/// <returns>Returns an object representing the property.</returns>
		ICollectionEntry<E, I> Collection<I>(Expression<Func<E, ICollection<I>>> propertySelector) where I : class;
	}
}
