using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Thrown when a foreign key constraint violation is attempted.
	/// </summary>
	[Serializable]
	public class ReferentialConstraintViolationException : IntegrityViolationException
	{
		private const string DefaultMessage = "Referential constraint violation.";
		
		/// <summary>
		/// Create with a default message.
		/// </summary>
		public ReferentialConstraintViolationException() : this(DefaultMessage) { }

		/// <summary>
		/// Create with a default message.
		/// </summary>
		/// <param name="inner">The inner exception.</param>
		public ReferentialConstraintViolationException(Exception inner) : this(DefaultMessage, inner) { }

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="message">The message.</param>
		public ReferentialConstraintViolationException(string message) : base(message) { }

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="inner">The inner exception.</param>
		public ReferentialConstraintViolationException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Used for serialization.
		/// </summary>
		protected ReferentialConstraintViolationException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
