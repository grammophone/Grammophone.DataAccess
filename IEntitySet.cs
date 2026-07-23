using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// A set of entities of type <typeparamref name="E"/>.
	/// An <see cref="IEntitySet{E}"/> represents the collection of 
	/// all entities in a container, or that can be queried from the database, 
	/// of a given type <typeparamref name="E"/>. 
	/// </summary>
	/// <typeparam name="E">The type of the entities.</typeparam>
	public interface IEntitySet<E> : IEntityQuery<E>
		where E : class
	{
		/// <summary>
		/// Adds the given entity to the container underlying the set
		/// in the Added state such that it will be inserted into the database when
		/// <see cref="IDomainContainer.SaveChanges"/> is called.
		/// </summary>
		/// <param name="entity">The entity to add.</param>
		/// <remarks>
		/// <para>
		/// This method states that the entity belongs to the unit of work, it does not schedule an insertion.
		/// Adding an entity which the container already tracks therefore changes nothing, whether it has been
		/// stored in the meantime or not.
		/// </para>
		/// <para>
		/// This matters because <see cref="IDomainContainer.BeginTransaction()"/> scopes nest and each of them
		/// saves when it commits, so an entity may well have been stored by an inner scope before control returns
		/// to the code which adds it, and that code has no way of telling. Were adding to be taken literally, the
		/// entity would be stored a second time: under the native providers that means either a refused insertion
		/// of an already assigned key, or a duplicate row silently created under a fresh key, carrying the entity
		/// at hand over to it and away from the row it was read from.
		/// </para>
		/// <para>
		/// Adding an entity which is being deleted is refused, because it could mean either abandoning the
		/// deletion or storing a second entity and the two are not interchangeable. An entity which has already
		/// been deleted, on the other hand, is no longer tracked and is therefore indistinguishable from any
		/// other detached entity carrying an assigned key, so adding it is left to the underlying provider.
		/// </para>
		/// </remarks>
		/// <exception cref="DataAccessException">Thrown when the entity is being deleted.</exception>
		void Add(E entity);

		/// <summary>
		/// Adds the given collection of entities into the set
		/// with each entity being put into the Added state such that it
		/// will be inserted into the database
		/// when <see cref="IDomainContainer.SaveChanges"/> is called.
		/// </summary>
		/// <param name="entities"></param>
		/// <remarks>
		/// Each entity is treated as in <see cref="Add(E)"/>, so those which the container already tracks
		/// are left alone.
		/// </remarks>
		void AddRange(IEnumerable<E> entities);

		/// <summary>
		/// Attaches the given entity to the container underlying the set.
		/// That is, the entity is placed into the container in the Unchanged state,
		/// just as if it had been read from the database.
		/// </summary>
		/// <param name="entity">The entity to attach.</param>
		void Attach(E entity);

		/// <summary>
		/// Create an entity of type <typeparamref name="E"/>.
		/// Note that this instance is NOT added or attached to the set.
		/// The instance returned will be a proxy if the underlying container 
		/// is configured to create proxies and the entity type meets 
		/// the requirements for creating a proxy. 
		/// </summary>
		/// <returns>Returns the new entity.</returns>
		E Create();

		/// <summary>
		/// Create an entity of type <typeparamref name="E"/>
		/// or a descendant type.
		/// Note that this instance is NOT added or attached to the set.
		/// The instance returned will be a proxy if the underlying container 
		/// is configured to create proxies and the entity type meets 
		/// the requirements for creating a proxy. 
		/// </summary>
		/// <typeparam name="T">
		/// The type of entity to create.
		/// It must be derived from <typeparamref name="E"/>.
		/// </typeparam>
		/// <returns>Returns the new entity.</returns>
		T Create<T>() where T : class, E;

		/// <summary>
		/// Finds an entity with the given primary key values.
		/// If an entity with the given primary key values exists in the container,
		/// then it is returned immediately without making a request to the store.
		/// Otherwise, a request is made to the store for an entity with 
		/// the given primary key values and this entity, if found,
		/// is attached to the container and returned.
		/// If no entity is found in the container or the store, then null is returned.
		/// </summary>
		/// <param name="keys">The values of the primary key for the entity to be found.</param>
		/// <returns>The entity found, or null.</returns>
		E Find(params object[] keys);

		/// <summary>
		/// Marks the given entity as Deleted such that it will be deleted 
		/// from the database when SaveChanges is called.
		/// Note that the entity must exist in the container in some other state
		/// before this method is called. 
		/// </summary>
		/// <param name="entity">The entity to remove.</param>
		void Remove(E entity);

		/// <summary>
		/// Removes the given collection of entities from the container underlying 
		/// the set with each entity being put into the Deleted state such that
		/// it will be deleted from the database when SaveChanges is called.
		/// </summary>
		/// <param name="entities">The entities to remove.</param>
		void RemoveRange(IEnumerable<E> entities);
	}
}
