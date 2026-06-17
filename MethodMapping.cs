using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Base specification for mapping a portable method expression to a native expression which a provider can interpret.
	/// </summary>
	public abstract class MethodMapping
	{
		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="portableMethodInfo">
		/// The portable method info of the method to be translated. If the method is generic, this is the generic method info.
		/// </param>
		/// <exception cref="ArgumentException">
		/// Thrown when <paramref name="portableMethodInfo"/> is generic and specialized.
		/// </exception>
		public MethodMapping(MethodInfo portableMethodInfo)
		{
			if (portableMethodInfo == null) throw new ArgumentNullException(nameof(portableMethodInfo));

			if (portableMethodInfo.IsGenericMethod && !portableMethodInfo.IsGenericMethodDefinition)
				throw new ArgumentException("The generic portable method info should not be specialized.", nameof(portableMethodInfo));

			this.PortableMethodInfo = portableMethodInfo;
		}

		/// <summary>
		/// The portable method info of the method to be translated. If the method is generic, this is the generic method info.
		/// </summary>
		public MethodInfo PortableMethodInfo { get; }

		/// <summary>
		/// Callback to translate an expression matching the <see cref="PortableMethodInfo"/>.
		/// </summary>
		/// <param name="originalMethodInfo">
		/// The method info that was matched in the original expression. Note that if the method is generic, this is the specialized method info, 
		/// unlike the generic definition in <see cref="PortableMethodInfo"/>.
		/// </param>
		/// <param name="arguments">The collection of argument expression arguments passed to the method expression.</param>
		/// <returns>
		/// Returns the transformed method call expression.
		/// </returns>
		public abstract MethodCallExpression MapMethodCallExpression(MethodInfo originalMethodInfo, IEnumerable<Expression> arguments);
	}
}
