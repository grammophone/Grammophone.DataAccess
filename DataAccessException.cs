using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Exception for an error in the data access layer.
	/// </summary>
	[Serializable]
	public class DataAccessException : SystemException
	{
		/// <summary>
		/// Create.
		/// </summary>
		public DataAccessException() { }

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="message">The exception message.</param>
		public DataAccessException(string message) : base(message) { }

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="message">The exception message.</param>
		/// <param name="inner">The inner exception.</param>
		public DataAccessException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Used for serialization.
		/// </summary>
		protected DataAccessException(
		System.Runtime.Serialization.SerializationInfo info,
		System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
