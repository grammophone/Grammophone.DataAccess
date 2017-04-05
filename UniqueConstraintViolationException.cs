using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Thrown when a duplicate is attempted for a primary key, unique constraint or unique index.
	/// </summary>
	[Serializable]
	public class UniqueConstraintViolationException : IntegrityViolationException
	{
		private static string DefaultMessage = "Unique constraint violation.";

		/// <summary>
		/// Create with default message.
		/// </summary>
		public UniqueConstraintViolationException() : this(DefaultMessage) { }

		/// <summary>
		/// Create with default message.
		/// </summary>
		/// <param name="inner">The inner exception.</param>
		public UniqueConstraintViolationException(Exception inner) : this(DefaultMessage, inner) { }

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="message">The message.</param>
		public UniqueConstraintViolationException(string message) : base(message) { }

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="inner">The inner exception.</param>
		public UniqueConstraintViolationException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// USed for serialization.
		/// </summary>
		protected UniqueConstraintViolationException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
