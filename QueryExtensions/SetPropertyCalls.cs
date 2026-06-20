using System;

namespace Grammophone.DataAccess.QueryExtensions
{
	/// <summary>
	/// Supports specifying properties and values for <see cref="SetOperationQueryMethods.ExecuteUpdate{T}"/>.
	/// </summary>
	/// <typeparam name="T">The entity type being updated.</typeparam>
	/// <remarks>
	/// This type is used only inside expression trees. It has no runtime implementation.
	/// </remarks>
	public sealed class SetPropertyCalls<T>
	{
		#region Construction

		private SetPropertyCalls()
		{
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Specifies a property and the value expression it should be updated to.
		/// </summary>
		/// <typeparam name="TProperty">The property type.</typeparam>
		/// <param name="propertyExpression">A property access expression.</param>
		/// <param name="valueExpression">A value expression.</param>
		/// <returns>The same instance so that multiple calls can be chained.</returns>
		public SetPropertyCalls<T> SetProperty<TProperty>(
			Func<T, TProperty> propertyExpression,
			Func<T, TProperty> valueExpression)
		{
			throw CreateNotSupportedException();
		}

		/// <summary>
		/// Specifies a property and the constant value it should be updated to.
		/// </summary>
		/// <typeparam name="TProperty">The property type.</typeparam>
		/// <param name="propertyExpression">A property access expression.</param>
		/// <param name="valueExpression">The value to assign.</param>
		/// <returns>The same instance so that multiple calls can be chained.</returns>
		public SetPropertyCalls<T> SetProperty<TProperty>(
			Func<T, TProperty> propertyExpression,
			TProperty valueExpression)
		{
			throw CreateNotSupportedException();
		}

		#endregion

		#region Private methods

		private static NotSupportedException CreateNotSupportedException()
		{
			return new NotSupportedException(
				$"{nameof(SetProperty)} can only be used inside translated set-based update expressions.");
		}

		#endregion
	}
}
