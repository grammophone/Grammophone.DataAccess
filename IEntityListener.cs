using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Used to track reading and writing entities.
	/// </summary>
	public interface IEntityListener
	{
		/// <summary>
		/// Called before adding an entity.
		/// </summary>
		void OnAdding(object entity);

		/// <summary>
		/// Called before deleting an entity.
		/// </summary>
		void OnDeleting(object entity);

		/// <summary>
		/// Called before changing an entity.
		/// </summary>
		void OnChanging(object entity);

		/// <summary>
		/// Called after materialization of an entity.
		/// </summary>
		void OnRead(object entity);
	}
}
