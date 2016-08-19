using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Specifies that an object holds a storage context.
	/// </summary>
	public interface IContextOwner
	{
		/// <summary>
		/// The underlying context which provides the access to the data.
		/// </summary>
		object UnderlyingContext { get; }
	}
}
