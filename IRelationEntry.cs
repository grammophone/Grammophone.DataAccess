using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Repreesents a relation property of an entity, such as a reference or a collection.
	/// </summary>
	/// <typeparam name="E">The type of the entity.</typeparam>
	/// <typeparam name="P">The type of the relation.</typeparam>
	public interface IRelationEntry<E, P> : IMemberEntry<E, P>
		where E : class
	{
		/// <summary>
		/// Determines or sets whether the relation has been loaded from the database.
		/// </summary>
		bool IsLoaded { get; set; }

		/// <summary>
		/// Loads the relation from the database. 
		/// Note that entities that already exist in the <see cref="IDomainContainer"/> 
		/// are not overwritten with values from the database.
		/// </summary>
		void Load();

		/// <summary>
		/// Asynchronously loads the relation from the database. 
		/// Note that entities that already exist in the <see cref="IDomainContainer"/> 
		/// are not overwritten with values from the database.
		/// </summary>
		Task LoadAsync();

		/// <summary>
		/// Asynchronously loads the relation from the database. 
		/// Note that entities that already exist in the <see cref="IDomainContainer"/> 
		/// are not overwritten with values from the database.
		/// </summary>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		Task LoadAsync(CancellationToken cancellationToken);
	}
}
