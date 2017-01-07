using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// The state of an entity.
	/// </summary>
	public enum TrackingState
	{
		/// <summary>
		/// The entity is not being tracked.
		/// </summary>
		Detached,

		/// <summary>
		/// The entity is being tracked but does not yet exist in the database.
		/// </summary>
		Added,

		/// <summary>
		/// The entity is being tracked by the context and exists in the database, 
		/// but has been marked for deletion.
		/// </summary>
		Deleted,

		/// <summary>
		/// The entity is being tracked and exists in the database, 
		/// and some or all of its property values have been modified. 
		/// </summary>
		Modified,

		/// <summary>
		/// The entity is being tracked and exists in the database, 
		/// and its property values have not changed from the values in the database. 
		/// </summary>
		Unchanged
	}
}
