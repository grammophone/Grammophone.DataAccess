using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Reports and alters change tracking.
	/// </summary>
	public interface IChangeTracker
	{
		/// <summary>
		/// Manually scan tracked entities for changes. Only necessary when <see cref="IDomainContainer.IsProxyCreationEnabled"/>
		/// is false or when an entity does not have all properties as virtual.
		/// </summary>
		void DetectChanges();

		/// <summary>
		/// True when the tracked entities have unsaved changes or when new entities are to be saved or when entities are to be deleted.
		/// </summary>
		bool HasChanges();

		/// <summary>
		/// Undo any changes to tracked entities.
		/// In particular, revert values of changes entities, detach new entities, and cancel deleting entities.
		/// </summary>
		void UndoChanges();

		/// <summary>
		/// Get the entities being tracked.
		/// </summary>
		/// <returns>Returns a collection of the tracked entities.</returns>
		IEnumerable<IEntityEntry<object>> Entries();

		/// <summary>
		/// Get the entities of type <typeparamref name="E"/> being tracked.
		/// </summary>
		/// <typeparam name="E">The type of the entities being tracked.</typeparam>
		/// <returns>Returns a collection of the specified tracked entities.</returns>
		IEnumerable<IEntityEntry<E>> Entries<E>() where E : class;

		/// <summary>
		/// Get the entities of type being tracked with specified tracking states.
		/// </summary>
		/// <param name="trackingState">Combination of <see cref="TrackingState"/> values via OR.</param>
		/// <returns>Returns a collection of the specified tracked entities.</returns>
		IEnumerable<IEntityEntry<object>> Entries(TrackingState trackingState);

		/// <summary>
		/// Get the entities of type <typeparamref name="E"/> being tracked with specified tracking states.
		/// </summary>
		/// <typeparam name="E">The type of the entities being tracked.</typeparam>
		/// <param name="trackingState">Combination of <see cref="TrackingState"/> values via OR.</param>
		/// <returns>Returns a collection of the specified tracked entities.</returns>
		IEnumerable<IEntityEntry<E>> Entries<E>(TrackingState trackingState) where E : class;
	}
}
