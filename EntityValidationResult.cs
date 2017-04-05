using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Validation result for a single entity.
	/// </summary>
	[Serializable]
	public class EntityValidationResult
	{
		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="entityEntry">The entry for the entity where the validation result applies.</param>
		/// <param name="isValid">True when the entity is valid.</param>
		/// <param name="validationErrors">The colection of validation errors.</param>
		public EntityValidationResult(
			IEntityEntry<object> entityEntry,
			bool isValid, 
			IReadOnlyCollection<EntityValidationError> validationErrors)
		{
			if (entityEntry == null) throw new ArgumentNullException(nameof(entityEntry));
			if (validationErrors == null) throw new ArgumentNullException(nameof(validationErrors));

			this.IsValid = isValid;
			this.ValidationErrors = validationErrors;
		}

		/// <summary>
		/// The entry for the entity where the validation result applies.
		/// </summary>
		public IEntityEntry<object> EntityEntry { get; private set; }

		/// <summary>
		/// Indicates whether the entity is valid.
		/// </summary>
		public bool IsValid { get; private set; }

		/// <summary>
		/// The colection of validation errors.
		/// </summary>
		public IReadOnlyCollection<EntityValidationError> ValidationErrors { get; private set; }
	}
}
