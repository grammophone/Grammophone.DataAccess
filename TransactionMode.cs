using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Transaction implementation.
	/// </summary>
	public enum TransactionMode
	{
		/// <summary>
		/// Actual underlying 'Commit' and 'SaveChanges' are invoked in corresponding methods.
		/// </summary>
		Real,

		/// <summary>
		/// Within transactions, 'Commit' and 'SaveChanges' methods are deferred until a top-level transaction commits.
		/// This means that database-generated columns such as auto-increments are not updated until after the final commit.
		/// This behavior is aimed for transient fault environments to enable retries.
		/// </summary>
		Deferred
	}
}
