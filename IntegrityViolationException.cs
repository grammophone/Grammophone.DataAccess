using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Exception thrown when a violation of the integrity of the data
	/// is attempted.
	/// </summary>
	[Serializable]
	public class IntegrityViolationException : DataAccessException
	{
		private const string DefaultMessage = "Data integrity violation.";

		/// <summary>
		/// Create with a default messsage.
		/// </summary>
		public IntegrityViolationException() : this(DefaultMessage) { }

		/// <summary>
		/// Create with a default messsage.
		/// </summary>
		/// <param name="inner">The inner exception cause.</param>
		public IntegrityViolationException(Exception inner) : this(DefaultMessage, inner) { }

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="message">The exception message.</param>
		public IntegrityViolationException(string message) : base(message) { }

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="message">The exception message.</param>
		/// <param name="inner">The inner exception cause.</param>
		public IntegrityViolationException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Used for serialization.
		/// </summary>
		protected IntegrityViolationException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
