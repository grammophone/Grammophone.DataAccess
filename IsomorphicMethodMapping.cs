using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Maps expressions of a portable method into one of a native method that has the same arguments signature.
	/// </summary>
	/// <remarks>
	/// The native method should have a return type compatible with the portable method being replaced.
	/// </remarks>
	public class IsomorphicMethodMapping : MethodMapping
	{
		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="portableMethodInfo">
		/// The portable method info of the method to be translated. If the method is generic, this is the generic method info.
		/// </param>
		/// <param name="nativeMethodInfo">
		/// The native method info of the method to replace the portable method. If the method is generic, this is the generic method info.
		/// </param>
		/// <exception cref="ArgumentException">
		/// Thrown when either <paramref name="portableMethodInfo"/> or <paramref name="nativeMethodInfo"/> are generic and specialized.
		/// </exception>
		public IsomorphicMethodMapping(MethodInfo portableMethodInfo, MethodInfo nativeMethodInfo)
			: base(portableMethodInfo)
		{
			if (nativeMethodInfo == null) throw new ArgumentNullException(nameof(nativeMethodInfo));

			if (nativeMethodInfo.IsGenericMethod && !nativeMethodInfo.IsGenericMethodDefinition)
				throw new ArgumentException("The generic native method info should not be specialized.", nameof(nativeMethodInfo));

			this.NativeMethodInfo = nativeMethodInfo;
		}

		/// <summary>
		/// The native method info to use as a replacement for the <see cref="MethodMapping.PortableMethodInfo"/>.
		/// </summary>
		public MethodInfo NativeMethodInfo { get; }

		/// <summary>
		/// Translates method call expressions of <see cref="MethodMapping.PortableMethodInfo"/> into <see cref="NativeMethodInfo"/>.
		/// </summary>
		public override Expression MapMethodCallExpression(MethodInfo originalMethodInfo, IEnumerable<Expression> arguments)
		{
			if (originalMethodInfo == null) throw new ArgumentNullException(nameof(originalMethodInfo));
			if (arguments == null) throw new ArgumentNullException(nameof(arguments));

			MethodInfo mappedMethodInfo = this.NativeMethodInfo;

			if (mappedMethodInfo.IsGenericMethodDefinition)
			{
				if (!originalMethodInfo.IsGenericMethod)
				{
					throw new ArgumentException(
						"The original method info must be generic when the native method info is generic.",
						nameof(originalMethodInfo));
				}

				mappedMethodInfo = mappedMethodInfo.MakeGenericMethod(originalMethodInfo.GetGenericArguments());
			}

			return Expression.Call(mappedMethodInfo, arguments);
		}
	}
}
