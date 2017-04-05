using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Thrown when validation of entities fails during saving to the database.
	/// </summary>
	[Serializable]
	public class EntityValidationException : DataAccessException
	{
		/// <summary>
		/// Create.
		/// </summary>
		public EntityValidationException() : this("Entity validation error.") { }

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="message">The exception message.</param>
		public EntityValidationException(string message) : this(message, null) { }

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="message">The exception message.</param>
		/// <param name="inner">The inner exception.</param>
		public EntityValidationException(string message, Exception inner) : this(message, inner, null) { }

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="message">The exception message.</param>
		/// <param name="inner">The inner exception.</param>
		/// <param name="validationResults">The validation results.</param>
		public EntityValidationException(
			string message, 
			Exception inner, 
			IEnumerable<EntityValidationResult> validationResults)
			: base(message, inner)
		{
			if (validationResults == null) validationResults = Enumerable.Empty<EntityValidationResult>();

			this.ValidationResults = validationResults;
		}

		/// <summary>
		/// user for serialization.
		/// </summary>
		protected EntityValidationException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

		/// <summary>
		/// The validation results.
		/// </summary>
		public IEnumerable<EntityValidationResult> ValidationResults { get; private set; }
	}
}
