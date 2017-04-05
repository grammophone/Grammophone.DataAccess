using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Validation error. Can be either entity or property level validation error.
	/// </summary>
	[Serializable]
	public class EntityValidationError
	{
		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="propertyName">The name of the invalid proeprty.</param>
		/// <param name="errorMessage">The validation error message.</param>
		public EntityValidationError(string propertyName, string errorMessage)
		{
			this.PropertyName = propertyName;
			this.ErrorMessage = errorMessage;
		}

		/// <summary>
		/// The name of the invalid proeprty.
		/// </summary>
		public string PropertyName { get; private set; }

		/// <summary>
		/// The validation error message.
		/// </summary>
		public string ErrorMessage { get; private set; }
	}
}
