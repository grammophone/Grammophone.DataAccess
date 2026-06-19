using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Rewrites portable method call expressions using registered method mappings.
	/// </summary>
	public class MethodMappingExpressionVisitor : ExpressionVisitor
	{
		#region Private fields

		private readonly IReadOnlyDictionary<MethodInfo, MethodMapping> methodMappingsByMethodInfo;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="methodMappingsByMethodInfo">Method mappings keyed by portable method info.</param>
		public MethodMappingExpressionVisitor(IReadOnlyDictionary<MethodInfo, MethodMapping> methodMappingsByMethodInfo)
		{
			if (methodMappingsByMethodInfo == null) throw new ArgumentNullException(nameof(methodMappingsByMethodInfo));

			this.methodMappingsByMethodInfo = methodMappingsByMethodInfo;
		}

		#endregion

		#region Protected methods

		/// <inheritdoc/>
		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if (node == null) throw new ArgumentNullException(nameof(node));

			var visitedObject = Visit(node.Object);
			var visitedArguments = node.Arguments.Select(Visit).ToArray();
			var methodInfo = GetMethodInfoKey(node.Method);

			if (TryGetMethodMapping(methodInfo, out var methodMapping))
			{
				return methodMapping.MapMethodCallExpression(node.Method, visitedArguments);
			}

			if (visitedObject != node.Object || !visitedArguments.SequenceEqual(node.Arguments))
			{
				return node.Update(visitedObject, visitedArguments);
			}

			return node;
		}

		#endregion

		#region Private methods

		private static MethodInfo GetMethodInfoKey(MethodInfo methodInfo)
		{
			if (methodInfo.IsGenericMethod && !methodInfo.IsGenericMethodDefinition)
			{
				return methodInfo.GetGenericMethodDefinition();
			}

			return methodInfo;
		}

		private bool TryGetMethodMapping(MethodInfo methodInfo, out MethodMapping methodMapping)
		{
			if (methodMappingsByMethodInfo.TryGetValue(methodInfo, out methodMapping))
			{
				return true;
			}

			foreach (var mapping in methodMappingsByMethodInfo.Values)
			{
				if (AreEquivalent(mapping.PortableMethodInfo, methodInfo))
				{
					methodMapping = mapping;
					return true;
				}
			}

			methodMapping = null;

			return false;
		}

		private static bool AreEquivalent(MethodInfo x, MethodInfo y)
		{
			return x == y
				|| x.Equals(y)
				|| (x.Module == y.Module && x.MetadataToken == y.MetadataToken)
				|| AreStructurallyEquivalent(x, y);
		}

		private static bool AreStructurallyEquivalent(MethodInfo x, MethodInfo y)
		{
			if (x.Name != y.Name) return false;
			if (x.DeclaringType != y.DeclaringType) return false;
			if (x.GetGenericArguments().Length != y.GetGenericArguments().Length) return false;

			return x.GetParameters().Length == y.GetParameters().Length;
		}

		#endregion
	}
}
