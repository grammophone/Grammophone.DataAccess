using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Abstract repository of entities.
	/// </summary>
	public interface IDomainContainer : IDisposable, IContextOwner
	{
		/// <summary>
		/// Save changes.
		/// </summary>
		/// <returns>Returns the number of objects written to the storage.</returns>
		/// <remarks>
		/// When in a transaction while <see cref="TransactionMode"/> is <see cref="TransactionMode.Deferred"/>, 
		/// this method does nothing and returns zero.
		/// </remarks>
		int SaveChanges();

		/// <summary>
		/// Save changes asynchronously.
		/// </summary>
		/// <returns>
		/// Returns a task whose result is the number of objects written to the storage.
		/// </returns>
		/// <remarks>
		/// When in a transaction while <see cref="TransactionMode"/> is <see cref="TransactionMode.Deferred"/>, 
		/// this method does nothing and returns zero.
		/// </remarks>
		Task<int> SaveChangesAsync();

		/// <summary>
		/// Save changes asynchronously.
		/// </summary>
		/// <param name="cancellationToken">
		/// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
		/// </param>
		/// <returns>
		/// Returns a task whose result is the number of objects written to the storage.
		/// </returns>
		/// <remarks>
		/// When in a transaction while <see cref="TransactionMode"/> is <see cref="TransactionMode.Deferred"/>, 
		/// this method does nothing and returns zero.
		/// </remarks>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Set the state of an entity as 'modified'.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <remarks>
		/// This is useful for services data transfer,
		/// and it typically follows the attachment of
		/// a deserialized <paramref name="entity"/>.
		/// </remarks>
		void SetAsModified(object entity);

		/// <summary>
		/// Set the state of a detached object graph as 'modified'.
		/// If the graph elements  are in any other state than 'detached',
		/// their state is not changed.
		/// WARNING: Setting any of the graph's disconnected elements' 
		/// relationships to a connected object
		/// changes all the 'disconnected' states to 'added'. Therefore, set all relationships
		/// to connected objects AFTER the call to this method.
		/// </summary>
		/// <typeparam name="T">The type of the root of the graph.</typeparam>
		/// <param name="graphRoot">the root of the graph.</param>
		void AttachGraphAsModified<T>(T graphRoot)
			where T : class;

		/// <summary>
		/// Detach a tracked entity.
		/// </summary>
		/// <param name="entity">The entity to detach.</param>
		/// <remarks>
		/// If the entity is not tracked, this method does nothing.
		/// </remarks>
		void Detach(object entity);

		/// <summary>
		/// Begins a local transaction on the underlying store.
		/// </summary>
		/// <returns>Returns an <see cref="IDisposable"/> transaction object.</returns>
		ITransaction BeginTransaction();

		/// <summary>
		/// Begins a local transaction on the underlying store 
		/// using the specified isolation level.
		/// </summary>
		/// <param name="isolationLevel">The requested isolation level.</param>
		/// <returns>Returns an <see cref="IDisposable"/> transaction object.</returns>
		/// <remarks>
		/// If the <see cref="TransactionMode"/> is <see cref="TransactionMode.Deferred"/>,
		/// the <paramref name="isolationLevel"/> parameter is ignored.
		/// </remarks>
		ITransaction BeginTransaction(IsolationLevel isolationLevel);

		/// <summary>
		/// Collection of entity listeners.
		/// </summary>
		ICollection<IEntityListener> EntityListeners { get; }

		/// <summary>
		/// Create a container proxy for a new object of type <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of the object to be proxied.</typeparam>
		/// <returns>
		/// Returns a proxy for the new object if <see cref="IsProxyCreationEnabled"/>
		/// is true, else returns a pure object.
		/// </returns>
		T Create<T>() where T : class;

		/// <summary>
		/// If set as true and all preconditions are met, the container
		/// will provide proxy classes wherever applicable. Default is true.
		/// </summary>
		bool IsProxyCreationEnabled { get; set; }

		/// <summary>
		/// If true, lazy loading is enabled. The default is true.
		/// </summary>
		bool IsLazyLoadingEnabled { get; set; }

		/// <summary>
		/// The transaction behavior.
		/// </summary>
		TransactionMode TransactionMode { get; }
	}
}
