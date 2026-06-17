using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Signature of a callback for a method call expression translation.
	/// </summary>
	/// <param name="originalMethodInfo">
	/// The method info that was matched in the original expression. Note that if the method is generic, this is the specialized method info, 
	/// unlike the generic definition in <see cref="MethodMapping.PortableMethodInfo"/>.
	/// </param>
	/// <param name="arguments">The collection of argument expression arguments passed to the method expression.</param>
	/// <returns>
	/// Returns the transformed method call expression.
	/// </returns>
	public delegate MethodCallExpression MethodExpressionMapper(
		MethodInfo originalMethodInfo,
		IEnumerable<Expression> arguments);

	/// <summary>
	/// Mapping of a method call expression using a specified callback.
	/// </summary>
	public class ExpressionMethodMapping : MethodMapping
	{
		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="portableMethodInfo">
		/// The portable method info of the method to be translated. If the method is generic, this is the generic method info.
		/// </param>
		/// <param name="methodExpressionMapper">Callback for a method call expression translation.</param>
		/// <exception cref="ArgumentException">
		/// Thrown when <paramref name="portableMethodInfo"/> is generic and specialized.
		/// </exception>
		public ExpressionMethodMapping(MethodInfo portableMethodInfo, MethodExpressionMapper methodExpressionMapper)
			: base(portableMethodInfo)
		{
			if (methodExpressionMapper == null) throw new ArgumentNullException(nameof(methodExpressionMapper));

			this.MethodExpressionMapper = methodExpressionMapper;
		}

		/// <summary>
		/// Callback for a method call expression translation.
		/// </summary>
		public MethodExpressionMapper MethodExpressionMapper { get; }

		/// <summary>
		/// Implements the method call expression mapping via <see cref="MethodExpressionMapper"/>.
		/// </summary>
		public override MethodCallExpression MapMethodCallExpression(MethodInfo originalMethodInfo, IEnumerable<Expression> arguments) 
			=> this.MethodExpressionMapper(originalMethodInfo, arguments);
	}
}
